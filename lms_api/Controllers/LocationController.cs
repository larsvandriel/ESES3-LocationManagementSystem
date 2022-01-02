using LocationManagementSystem.API.Filters;
using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities.Extensions;
using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.Parameters;
using LocationManagementSystem.Entities.ShapedEntities;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace LocationManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly LinkGenerator _linkGenerator;

        public LocationController(ILoggerManager logger, IRepositoryWrapper repository, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _repository = repository;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetLocations([FromQuery] LocationParameters locationParameters)
        {
            try
            {
                var locations = _repository.Location.GetAllLocations(locationParameters);

                var metadata = new
                {
                    locations.TotalCount,
                    locations.PageSize,
                    locations.CurrentPage,
                    locations.TotalPages,
                    locations.HasNext,
                    locations.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                _logger.LogInfo($"Returned {locations.Count} locations from database.");

                var shapedLocations = locations.Select(i => i.Entity).ToList();

                var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

                if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Ok(shapedLocations);
                }

                for (var index = 0; index < locations.Count; index++)
                {
                    var brandLinks = CreateLinksForLocation(locations[index].Id, locationParameters.Fields);
                    shapedLocations[index].Add("Links", brandLinks);
                }

                var locationsWrapper = new LinkCollectionWrapper<Entity>(shapedLocations);

                return Ok(CreateLinksForLocations(locationsWrapper));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllLocations action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "LocationById")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetLocationById(Guid id, [FromQuery] string fields)
        {
            try
            {
                var location = _repository.Location.GetLocationById(id, fields);

                if (location.Id == Guid.Empty)
                {
                    _logger.LogError($"Location with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

                if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogInfo($"Returned shaped location with id: {id}");
                    return Ok(location.Entity);
                }

                location.Entity.Add("Links", CreateLinksForLocation(location.Id, fields));

                return Ok(location.Entity);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wring inside GetLocationById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateLocation([FromBody] Location location)
        {
            try
            {
                if (location.IsObjectNull())
                {
                    _logger.LogError("Location object sent from client is null.");
                    return BadRequest("Location object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid location object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Location.CreateLocation(location);
                _repository.Save();

                return CreatedAtRoute("LocationById", new { id = location.Id }, location);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateLocation action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLocation(Guid id, [FromBody] Location location)
        {
            try
            {
                if (location.IsObjectNull())
                {
                    _logger.LogError("Location object sent from client is null.");
                    return BadRequest("Location object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid location object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbLocation = _repository.Location.GetLocationById(id);
                if (dbLocation.IsEmptyObject())
                {
                    _logger.LogError($"Location with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Location.UpdateLocation(dbLocation, location);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateLocation action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(Guid id)
        {
            try
            {
                var location = _repository.Location.GetLocationById(id);
                if (location.IsEmptyObject())
                {
                    _logger.LogError($"Location with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Location.DeleteLocation(location);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteLocation action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private IEnumerable<Link> CreateLinksForLocation(Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetLocationById), values: new {id, fields}), "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteLocation), values: new {id}), "delete_brand", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateLocation), values: new {id}), "update_brand", "PUT")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForLocations(LinkCollectionWrapper<Entity> locationsWrapper)
        {
            locationsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetLocations), values: new { }), "self", "GET"));

            return locationsWrapper;
        }
    }
}

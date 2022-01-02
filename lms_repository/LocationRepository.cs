using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities;
using LocationManagementSystem.Entities.Extensions;
using LocationManagementSystem.Entities.Helpers;
using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.Parameters;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Repository
{
    public class LocationRepository: RepositoryBase<Location>, ILocationRepository
    {
        private readonly ISortHelper<Location> _sortHelper;

        private readonly IDataShaper<Location> _dataShaper;

        public LocationRepository(RepositoryContext repositoryContext, ISortHelper<Location> sortHelper, IDataShaper<Location> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public void CreateLocation(Location location)
        {
            location.TimeCreated = DateTime.Now;
            Create(location);
        }

        public void DeleteLocation(Location location)
        {
            location.Deleted = true;
            location.TimeDeleted = DateTime.Now;
            Location dbLocation = GetLocationById(location.Id);
            UpdateLocation(dbLocation, location);
        }

        public PagedList<ShapedEntity> GetAllLocations(LocationParameters locationParameters)
        {
            var locations = FindByCondition(location => !location.Deleted);

            SearchByName(ref locations, locationParameters.Name);

            var sortedLocations = _sortHelper.ApplySort(locations, locationParameters.OrderBy);
            var shapedLocations = _dataShaper.ShapeData(sortedLocations, locationParameters.Fields).AsQueryable();

            return PagedList<ShapedEntity>.ToPagedList(shapedLocations, locationParameters.PageNumber, locationParameters.PageSize);
        }

        public ShapedEntity GetLocationById(Guid locationId, string fields)
        {
            var location = FindByCondition(location => location.Id.Equals(locationId)).FirstOrDefault();

            if (location == null)
            {
                location = new Location();
            }
            return _dataShaper.ShapeData(location, fields);
        }

        public Location GetLocationById(Guid locationId)
        {
            return FindByCondition(i => i.Id.Equals(locationId)).FirstOrDefault();
        }

        public void UpdateLocation(Location dbLocation, Location location)
        {
            dbLocation.Map(location);
            Update(dbLocation);
        }

        private void SearchByName(ref IQueryable<Location> locations, string locationName)
        {
            if (!locations.Any() || string.IsNullOrWhiteSpace(locationName))
            {
                return;
            }

            locations = locations.Where(i => i.Name.ToLower().Contains(locationName.Trim().ToLower()));
        }
    }
}

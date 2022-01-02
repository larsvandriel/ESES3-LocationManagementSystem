using LoggingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using LocationManagementSystem.API.Filters;
using LocationManagementSystem.Contracts;
using LocationManagementSystem.Entities;
using LocationManagementSystem.Entities.Helpers;
using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Repository;

namespace LocationManagementSystem.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMsSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mssqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<ISortHelper<Department>, SortHelper<Department>>();
            services.AddScoped<IDataShaper<Department>, DataShaper<Department>>();
            services.AddScoped<ISortHelper<Employee>, SortHelper<Employee>>();
            services.AddScoped<IDataShaper<Employee>, DataShaper<Employee>>();
            services.AddScoped<ISortHelper<Location>, SortHelper<Location>>();
            services.AddScoped<IDataShaper<Location>, DataShaper<Location>>();
            services.AddScoped<ISortHelper<Room>, SortHelper<Room>>();
            services.AddScoped<IDataShaper<Room>, DataShaper<Room>>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void RegisterFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.eses2.hateoas+json");
                }
                var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.eses2.hateoas+xml");
                }
            });
        }
    }
}

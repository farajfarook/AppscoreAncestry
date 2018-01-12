using System.Runtime.CompilerServices;
using AppscoreAncestry.Domain.Models;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Domain.Services;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppscoreAncestry.Infrastructure.Config
{
    public static class InfrastructureSetup
    {
        public static void AddAppscoreAncestryServices(this IServiceCollection services, IConfigurationSection settings)
        {
            services.Configure<Settings>(settings);
            services.AddTransient<IDataDetail, FileDataDetail>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();
            services.AddTransient<IDataAccess, FileDataAccess>();
            services.AddTransient<IPersonSearchService, PersonSearchService>();
        }
    }
}
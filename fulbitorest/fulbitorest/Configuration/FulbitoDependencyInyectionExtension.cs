using datalayer.Contracts;
using datalayer.Contracts.Repositories;
using datalayer.Repositories;
using FulbitoRest.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using Microsoft.Extensions.DependencyInjection;
using model.Model;

namespace FulbitoRest.Configuration
{
    public static class FulbitoDependencyInyectionExtension
    {
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddScoped<LoginService>(); //Scoped because of DbContext consume
            services.AddScoped<LocationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            services.AddScoped<LoggingFilterAttribute>();

            services.AddSingleton<IRepository<UserCredentials>, InMemoryRepository<UserCredentials>>();
        }
    }
}

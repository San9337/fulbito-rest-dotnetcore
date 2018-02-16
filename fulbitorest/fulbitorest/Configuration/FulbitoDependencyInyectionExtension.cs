using datalayer.Contracts;
using FulbitoRest.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using Microsoft.Extensions.DependencyInjection;
using model;

namespace FulbitoRest.Configuration
{
    public static class FulbitoDependencyInyectionExtension
    {
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddScoped<LoginService>(); //Scoped because of DbContext consume

            services.AddScoped<LoggingFilterAttribute>();

            services.AddSingleton<IRepository<UserCredentials>, InMemoryRepository<UserCredentials>>();
        }
    }
}

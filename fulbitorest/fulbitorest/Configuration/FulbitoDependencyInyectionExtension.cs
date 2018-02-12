﻿using datalayer.Contracts;
using FulbitoRest.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using FulbitoRest.Technical.Security;
using Microsoft.Extensions.DependencyInjection;
using model;

namespace FulbitoRest.Configuration
{
    public static class FulbitoDependencyInyectionExtension
    {
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddSingleton<LoginService>();

            services.AddScoped<LoggingFilterAttribute>();
            services.AddScoped<AuthenticateAttribute>();

            services.AddSingleton<IRepository<UserCredentials>, InMemoryRepository<UserCredentials>>();
        }
    }
}
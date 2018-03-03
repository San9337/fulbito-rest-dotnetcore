using datalayer.Contracts;
using datalayer.Contracts.Repositories;
using datalayer.Repositories;
using FulbitoRest.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using FulbitoRest.Technical.Security;
using Microsoft.Extensions.DependencyInjection;
using model.Model;
using model.Model.Security;
using System.Reflection;
using System.Linq;
using FulbitoRest.Services.Contracts;

namespace FulbitoRest.Configuration
{
    /// <summary>
    /// Registers all injected dependencies
    /// </summary>
    public static class FulbitoDependencyInyectionExtension
    {
        public static void AddDiServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddSingleton<RefreshTokenParser>();
            services.AddSingleton<IContentSecurityHelper, ContentSecurityHelper>();

            services.AddScoped<LoggingFilterAttribute>();

            services.RegisterRepositories();
            services.RegisterServices();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.RegisterTypesByInterfaceConvention("datalayer", "IRepository");
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            //services.RegisterTypesByInterfaceConvention("fulbitorest", "IService");

            services.AddScoped<LoginService>();
            services.AddScoped<LocationService>();
            services.AddScoped<IUserService,UserService>();
        }

        private static void RegisterTypesByInterfaceConvention(this IServiceCollection services, string assemblyname, string baseInterface)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            var datalayerAssembly = assemblies.First(a => a.Name == assemblyname);

            var allTypes = Assembly.Load(datalayerAssembly).DefinedTypes;

            foreach (var type in allTypes)
            {
                if (type.IsInterface && type.GetInterfaces().Any(t => t.Name.Contains(baseInterface)))
                {
                    var interfaceName = type.Name;
                    var implementationType = allTypes.First(t => t.Name == type.Name.Substring(1, type.Name.Length - 1));
                    services.AddScoped(type, implementationType);
                }
            }
        }
    }
}

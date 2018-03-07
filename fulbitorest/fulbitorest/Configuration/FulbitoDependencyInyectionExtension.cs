using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using FulbitoRest.Technical.Security;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

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
            services.AddSingleton<IRefreshTokenParser, RefreshTokenParser>();
            services.AddSingleton<IContentSecurityHelper, ContentSecurityHelper>();

            services.AddScoped<LoggingFilterAttribute>();

            services.RegisterRepositories();
            services.RegisterServices();
            services.RegisterHelpers();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.RegisterTypesByInterfaceConvention("datalayer", "IRepository");
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterTypesByInterfaceConvention("FulbitoRest", "IService");
        }
        private static void RegisterHelpers(this IServiceCollection services)
        {
            services.RegisterTypesByInterfaceConvention("FulbitoRest", "IHelper");
        }


        private static void RegisterTypesByInterfaceConvention(this IServiceCollection services, string assemblyname, string baseInterface)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();
            assemblies.Add(Assembly.GetEntryAssembly().GetName());

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

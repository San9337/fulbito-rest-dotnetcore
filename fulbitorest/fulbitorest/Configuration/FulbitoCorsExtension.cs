using Microsoft.Extensions.DependencyInjection;

namespace FulbitoRest.Configuration
{
    public static class FulbitoCorsExtension
    {
        public const string AllowAllPolicy = "AllowAll";

        public static void AddFulbitoCors(this IServiceCollection services)
        {
            //Register all policies
            services.AddCors(p => p.AddPolicy(AllowAllPolicy, policyBuilder =>
                policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                )
            );
        }
    }
}

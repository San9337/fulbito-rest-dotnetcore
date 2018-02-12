using datalayer.Contracts;
using FulbitoRest.Configuration;
using FulbitoRest.Hubs;
using FulbitoRest.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using FulbitoRest.Technical.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using model;

namespace Fulbito_Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFulbitoCors();
            services.AddFulbitoJsonWebTokens(Configuration);

            services
                .AddMvc()
                .AddJsonOptions(opt => {
                    opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });

            services.AddSignalR();

            services.AddDiServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors(FulbitoCorsExtension.AllowAllPolicy);
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                //Required for nginx proxy integration
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}"
                );
            });

            app.UseFileServer();
            app.UseSignalR(routes =>
            {
                //npm install @aspnet/signalr-client
                routes.MapHub<NotificationTestHub>(nameof(NotificationTestHub).Replace("Hub", "")); //Hub name used for registration
            });

        }
    }
}

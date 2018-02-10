using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpOverrides;
using FulbitoRest.Hubs;
using FulbitoRest.Technical.Logging;
using FulbitoRest.Technical.Interception;
using FulbitoRest.Services;
using FulbitoRest.Technical.Security;
using datalayer.Contracts;
using model;
using FulbitoRest.Repositories;

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
            services.AddCors(p => p.AddPolicy("AllowAllPolicy", policyBuilder =>
                policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                )
            );

            services.AddMvc();
            services.AddSignalR();

            AddDiServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseCors("AllowAllPolicy");
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

        private static void AddDiServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddSingleton<LoginService>();

            services.AddScoped<LoggingFilterAttribute>();
            services.AddScoped<AuthenticateAttribute>();

            services.AddSingleton<IRepository<UserCredentials>, InMemoryRepository<UserCredentials>>();
        }
    }
}

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
            {
                policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                ;
            }
            ));

            services.AddMvc();
            services.AddSignalR();
            services.AddSingleton<ICustomLogger, Logger>();
            services.AddScoped<LoggingFilterAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAllPolicy"
            //.WithOrigins("*")
            //.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS", "PATCH")
            //.WithExposedHeaders("Authorization", "Content-Type", "Accept", "Origin", "User-Agent", "DNT", "Cache-Control", "Keep-Alive", "X-Mx-ReqToken", "X-Requested-With", "If-Modified-Since")
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}"
                );
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationTestHub>(nameof(NotificationTestHub).Replace("Hub","")); //Hub name used for registration
            });
        }
    }
}

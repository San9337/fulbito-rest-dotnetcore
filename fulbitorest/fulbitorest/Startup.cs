using datalayer.FulbitoContext;
using FulbitoRest.Configuration;
using FulbitoRest.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Fulbito",
                    Version = "v1",
                    Description = "TFS: https://fulbito.visualstudio.com"
                });
            });

            services.AddDiServices();

            services.AddDbContext<FulbitoDbContext>(options => {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });
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

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fulbito v1");
            });
        }
    }
}

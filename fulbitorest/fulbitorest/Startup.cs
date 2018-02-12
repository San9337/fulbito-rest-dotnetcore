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
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

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

            AddJasonWebTokens(services);

            services
                .AddMvc(opt => {
                    var formatters = opt.InputFormatters;
                })
                .AddJsonOptions(opt => {
                    opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });
            services.AddSignalR();

            AddDiServices(services);
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

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

        private void AddJasonWebTokens(IServiceCollection services)
        {
            //https://medium.com/@lugrugzo/asp-net-core-2-0-webapi-jwt-authentication-with-identity-mysql-3698eeba6ff8
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;

                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"], //appsettings.json
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
        }

        public class RequestResponseLoggingMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger _logger;

            public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                    ILoggerFactory loggerFactory)
            {
                _next = next;
                _logger = loggerFactory
                          .CreateLogger<RequestResponseLoggingMiddleware>();
            }

            public async Task Invoke(HttpContext context)
            {
                Console.Write("Intercepted something");
                if (context.Request.ContentType.Equals("application/json"))
                {
                    string body = new StreamReader(context.Request.Body).ReadToEnd();

                    Console.WriteLine("SE RECIBIO: " + body.Replace("\n", "NN").Replace("\r", "RR"));
                    body = body.Replace("\r\n", "\n");

                    context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                }
                await _next(context);
            }
        }
    }
}

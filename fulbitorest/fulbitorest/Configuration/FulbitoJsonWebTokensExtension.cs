﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FulbitoRest.Configuration
{
    /// <summary>
    /// Configures the JWT authentication, setting it as the default schema
    /// </summary>
    public static class FulbitoJsonWebTokensExtension
    {
        public static void AddFulbitoAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                    jwtOptions.IncludeErrorDetails = true;

                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Jwt:Issuer"], //appsettings.json
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                        
                    };
                })
                //https://github.com/aspnet/Security/issues/1310
                //.AddFacebook(options => {
                    //maybe unnecesary, left as reference
                //})
                ;
        }
    }
}

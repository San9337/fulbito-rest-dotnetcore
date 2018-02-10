using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using FulbitoRest.Services;
using System.Net;

namespace FulbitoRest.Technical.Security
{
    public class AuthenticateAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private LoginService _loginService;
        public AuthenticateAttribute(LoginService loginService)
        {
            _loginService = loginService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var headers = httpContext.Request.Headers;

            var authHeader = headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();

            if (authHeader == "")
            {
                throw new UnauthorizedAccessException("Authorization header missing");
            }

            var userAndPass = authHeader.Split(":");

            if(userAndPass.Count() != 2)
            {
                throw new UnauthorizedAccessException("Authorization header format is incorrect");
            }

            var user = userAndPass[0];
            var pass = userAndPass[1];

            var credentials = _loginService.Login(user, pass);
            if(credentials == null)
            {
                throw new UnauthorizedAccessException("Invalid user credentials");
            }

            var connectionId = context.HttpContext.Connection.Id; //0HLBGND50HUS7
            var user2 = context.HttpContext.User;
        }


    }
}

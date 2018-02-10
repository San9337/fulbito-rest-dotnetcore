using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FulbitoRest.Services;
using System.Net.Http;
using apidata;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    //https://github.com/blowdart/AspNetAuthorizationWorkshop <-----Check for middleware implementation
    public class AccountController : Controller
    {
        private LoginService _loginService;
        public AccountController(LoginService login)
        {
            _loginService = login;
        }

        [HttpPost]
        public IActionResult Register([FromBody]UserCredentialsData credentials)
        {
            var newCredentials = _loginService.Register(credentials.User, credentials.Password);
            if (newCredentials == null)
                return Unauthorized();

            return Ok();
        }
    }
}
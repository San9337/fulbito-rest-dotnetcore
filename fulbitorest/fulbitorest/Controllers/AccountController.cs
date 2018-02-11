using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FulbitoRest.Services;
using System.Net.Http;
using apidata;
using model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    //https://github.com/blowdart/AspNetAuthorizationWorkshop <-----Check for middleware implementation
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;

        public AccountController(LoginService login, IConfiguration configuration)
        {
            _loginService = login;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<object> Register([FromBody]UserCredentialsData credentials)
        {
            var newCredentials = _loginService.Register(credentials.User, credentials.Password);
            if (newCredentials == null)
                throw new ApplicationException("Could not register user");

            return await GenerateJwtToken(newCredentials);
        }

        [HttpPost]
        public async Task<object> Login([FromBody]UserCredentialsData credentials)
        {
            var userCredentials = _loginService.Login(credentials.User, credentials.Password);
            if(userCredentials == null)
                throw new ApplicationException("Invalid login");

            return await GenerateJwtToken(userCredentials);
        }

        private async Task<object> GenerateJwtToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userCredentials.User),//TODO the email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userCredentials.User) //TODO The user Id 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
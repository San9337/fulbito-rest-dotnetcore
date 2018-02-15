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
using FulbitoRest.Controllers;
using FulbitoRest.Technical.Security;
using Microsoft.AspNetCore.Authentication;
using apidata.Mapping;
using System.Net;
using apidata.Responses;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    //https://github.com/blowdart/AspNetAuthorizationWorkshop <-----Check for middleware implementation
    public class AccountController : BaseController
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;

        public AccountController(LoginService login, IConfiguration configuration)
        {
            _loginService = login;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public TokenResponseData Register([FromBody]UserCredentialsData credentials)
        {
            var newCredentials = _loginService.Register(credentials.MapTo<UserCredentials>());

            return new TokenResponseData()
            {
                IsSuccess = true,
                Token = GenerateJwtToken(newCredentials),
            };
        }

        [HttpPost]
        [Route("login")]
        public TokenResponseData Login([FromBody]UserCredentialsData credentials)
        {
            var userCredentials = _loginService.Login(credentials.User, credentials.Password);
            if(userCredentials == null)
                throw new ApplicationException("Invalid redentials for login");

            return new TokenResponseData()
            {
                IsSuccess = true,
                Token = GenerateJwtToken(userCredentials),
            };
        }

        private string GenerateJwtToken(UserCredentials userCredentials)
        {
            var claims = FulbitoClaims.CreateClaims(userCredentials);
            var identity = new ClaimsIdentity(claims);
            HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"]));

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
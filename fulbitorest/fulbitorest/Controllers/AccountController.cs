using apidata.DataContracts;
using apidata.Responses;
using apidata.Utils;
using FulbitoRest.Controllers;
using FulbitoRest.Services;
using FulbitoRest.Technical.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using model.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            credentials.ValidateBody();

            var newCredentials = _loginService.Register(credentials.NickName, credentials.Email, credentials.Password);

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
            credentials.ValidateBody();

            var user = _loginService.Login(credentials.Email, credentials.Password);
            if(user == null)
                throw new ApplicationException("Invalid redentials for login");

            return new TokenResponseData()
            {
                IsSuccess = true,
                Token = GenerateJwtToken(user),
            };
        }

        private string GenerateJwtToken(User user)
        {
            var userCredentials = user.Credentials;

            var claims = FulbitoClaims.CreateClaims(user);
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
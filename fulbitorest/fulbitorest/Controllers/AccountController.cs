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
using model.Enums;
using model.Model;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

            return TokenResponseData.Success(GenerateJwtToken(newCredentials, AuthenticationMethod.Fulbito));
        }


        [HttpPost]
        [Route("login")]
        public TokenResponseData FulbitoLogin([FromBody]UserCredentialsData credentials)
        {
            credentials.ValidateBody();

            var user = _loginService.Login(credentials.Email, credentials.Password);
            if(user == null)
                throw new ApplicationException("Invalid redentials for login");

            return TokenResponseData.Success(GenerateJwtToken(user, AuthenticationMethod.Fulbito));
        }

        //https://leastprivilege.com/2013/12/23/advanced-oauth2-assertion-flow-why/
        //https://stackoverflow.com/questions/24180034/authenticated-access-to-webapi-via-facebook-token-from-android-app
        [HttpPost]
        [Route("facebook")]
        //[RequireHttps]
        public async Task<TokenResponseData> FacebookLogin([FromBody] string fbToken)
        {
            var user = await _loginService.LoginWithFacebook(fbToken);

            if (user == null)
                return TokenResponseData.Failure;

            return TokenResponseData.Success(GenerateJwtToken(user, AuthenticationMethod.Facebook));
        }

        private string GenerateJwtToken(User user, AuthenticationMethod method)
        {
            var userCredentials = user.Credentials;

            var claims = FulbitoClaims.CreateClaims(user, method);
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
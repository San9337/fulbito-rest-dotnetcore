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
using model.Model.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    //https://github.com/blowdart/AspNetAuthorizationWorkshop <-----Check for middleware implementation
    //http://www.c-sharpcorner.com/article/handle-refresh-token-using-asp-net-core-2-0-and-json-web-token/ <-----tokens
    public class AccountController : BaseController
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;
        private readonly RefreshTokenParser _tokenParser;

        public AccountController(LoginService login, IConfiguration configuration, RefreshTokenParser tokenParser)
        {
            _loginService = login;
            _configuration = configuration;
            _tokenParser = tokenParser;
        }

        [HttpPost]
        [Route("register")]
        public RefreshTokenResponseData Register([FromBody]UserCredentialsData credentials)
        {
            credentials.ValidateBody();

            var auth = _loginService.Register(credentials.NickName, credentials.Email, credentials.Password);

            return RefreshTokenResponseData.Success(
                access: GenerateJwtAccessToken(auth.User, AuthenticationMethod.Fulbito),
                refresh: GenerateSignedJwtRefreshToken(auth)
                );
        }


        [HttpPost]
        [Route("login")]
        public RefreshTokenResponseData FulbitoLogin([FromBody]UserCredentialsData credentials)
        {
            credentials.ValidateBody();

            var auth = _loginService.Login(credentials.Email, credentials.Password);
            if (auth == null)
                throw new ApplicationException("Invalid redentials for login");

            return RefreshTokenResponseData.Success(
                access: GenerateJwtAccessToken(auth.User, AuthenticationMethod.Fulbito),
                refresh: GenerateSignedJwtRefreshToken(auth)
            );
        }

        [HttpPost]
        [Route("auth")]
        public RefreshTokenResponseData Auth([FromBody]RefreshRequestData data)
        {
            data.ValidateBody();
            //grant_type	the value must be refresh_token
            //client_id the client_id is assigned by manager
            //client_secret   the client_secret is assigned by manager
            //refresh_token   after authentication the server will return a refresh_tokens
            try
            {
                var refreshToken = ValidateAndRetrieveRefreshToken(data.SignedRefreshToken);

                var authContext = _loginService.RefreshToken(refreshToken);

                return RefreshTokenResponseData.Success(
                    access: GenerateJwtAccessToken(authContext.User, authContext.AuthMethod),
                    refresh: GenerateSignedJwtRefreshToken(authContext)
                );
            }catch(SecurityException ex)
            {
                return RefreshTokenResponseData.Failure(ex.Message);
            }
        }

        //https://leastprivilege.com/2013/12/23/advanced-oauth2-assertion-flow-why/
        //https://stackoverflow.com/questions/24180034/authenticated-access-to-webapi-via-facebook-token-from-android-app
        [HttpPost]
        [Route("facebook")]
        //[RequireHttps]
        public async Task<RefreshTokenResponseData> FacebookLogin([FromBody] string fbToken)
        {
            try
            {
                var auth = await _loginService.LoginWithFacebook(fbToken);

                return RefreshTokenResponseData.Success(
                    access: GenerateJwtAccessToken(auth.User, AuthenticationMethod.Facebook),
                    refresh: GenerateSignedJwtRefreshToken(auth)
                );
            }catch(Exception ex)
            {
                return RefreshTokenResponseData.Failure(ex.Message);
            }
        }

        private string GenerateJwtAccessToken(User user, AuthenticationMethod method)
        {
            var fulbitoClaims = FulbitoClaims.CreateClaims(user, method);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                fulbitoClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"])),
                signingCredentials: ContentSecurityHelper.GetSigninCredentials(_configuration["JwtKey"])
            );

            SignInAsync(fulbitoClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Load the claims to the HttpContext
        /// </summary>
        private void SignInAsync(System.Collections.Generic.IEnumerable<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims);
            HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }

        private string GenerateSignedJwtRefreshToken(AuthContext authContext)
        {
            var key = _configuration["JwtRefreshKey"];
            var refreshToken = authContext.RefreshToken;

            return _tokenParser.FormatToken(refreshToken, key);
        }

        private string ValidateAndRetrieveRefreshToken(string tokenWithSignature)
        {
            var key = _configuration["JwtRefreshKey"];

            return _tokenParser.ValidateAndRetrieve(tokenWithSignature, key);
        }
    }
}
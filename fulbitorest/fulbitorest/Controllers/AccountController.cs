using apidata.DataContracts;
using apidata.Mapping;
using apidata.Responses;
using apidata.Utils;
using FulbitoRest.Controllers;
using FulbitoRest.Helpers.Contracts;
using FulbitoRest.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using model.Enums;
using model.Model;
using System;
using System.Security;
using System.Threading.Tasks;

//https://github.com/blowdart/AspNetAuthorizationWorkshop <-----Check for setting up roles and policies
//http://www.c-sharpcorner.com/article/handle-refresh-token-using-asp-net-core-2-0-and-json-web-token/ <-----jwt tokens standards
namespace fulbitorest.Controllers
{
    /// <summary>
    /// All security related endpoints go here
    /// </summary>
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly ILoginService _loginService;
        private readonly ITokenHelper _tokenHelper;

        public AccountController(
            ILoginService login, 
            ITokenHelper tokenHelper
        )
        {
            _loginService = login;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("register")]
        public RefreshTokenResponseData Register([FromBody]UserCredentialsData credentials)
        {
            credentials.ValidateBody();

            var auth = _loginService.Register(credentials.MapToFulbitoUser());

            return RefreshTokenResponseData.Success(
                access: SignInAccessToken(auth.User, AuthenticationMethod.Fulbito),
                refresh: _tokenHelper.CreateRefreshToken(auth)
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
                access: SignInAccessToken(auth.User, AuthenticationMethod.Fulbito),
                refresh: _tokenHelper.CreateRefreshToken(auth)
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
                var authContext = _loginService.RefreshToken(data.SignedRefreshToken);

                return RefreshTokenResponseData.Success(
                    access: SignInAccessToken(authContext.User, authContext.AuthMethod),
                    refresh: _tokenHelper.CreateRefreshToken(authContext)
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
        public async Task<RefreshTokenResponseData> FacebookLogin([FromBody] ThirdPartyLoginRequestData data)
        {
            data.ValidateBody();
            try
            {
                var auth = await _loginService.LoginWithFacebook(data.Token);

                return RefreshTokenResponseData.Success(
                    access: SignInAccessToken(auth.User, AuthenticationMethod.Facebook),
                    refresh: _tokenHelper.CreateRefreshToken(auth)
                );
            }catch(Exception ex)
            {
                return RefreshTokenResponseData.Failure(ex.Message);
            }
        }

        private string SignInAccessToken(User user, AuthenticationMethod method)
        {
            var tokenAndClaims = _tokenHelper.GenerateJwtAccessToken(user, method);
            HttpContext.SignInAsync(tokenAndClaims.ClaimsPrincipal);

            return tokenAndClaims.AccessToken;
        }
        
    }
}
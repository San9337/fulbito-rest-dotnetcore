using FulbitoRest.Helpers.Contracts;
using FulbitoRest.Technical.Security;
using Microsoft.Extensions.Configuration;
using model.Enums;
using model.Model;
using model.Model.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FulbitoRest.Helpers.Implementations
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenParser _tokenParser;

        public TokenHelper(
            IConfiguration configuration,
            IRefreshTokenParser tokenParser)
        {
            _configuration = configuration;
            _tokenParser = tokenParser;
        }

        public AccessTokenLogin GenerateJwtAccessToken(User user, AuthenticationMethod method)
        {
            var fulbitoClaims = FulbitoClaims.CreateClaims(user, method);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                fulbitoClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: ContentSecurityHelper.GetSigninCredentials(_configuration["Jwt:Key"])
            );

            var identity = new ClaimsIdentity(fulbitoClaims);

            return new AccessTokenLogin()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ClaimsPrincipal = new ClaimsPrincipal(identity),
            };
        }

        public string CreateRefreshToken(AuthContext authContext)
        {
            var refreshToken = authContext.RefreshToken;

            string key = GenerateRefreshTokenKey();

            return _tokenParser.FormatToken(refreshToken, key);
        }

        public string ValidateAndRemoveSignature(string tokenWithSignature)
        {
            var key = GenerateRefreshTokenKey();
            return _tokenParser.ValidateAndRemoveSignature(tokenWithSignature, key);
        }

        private string GenerateRefreshTokenKey()
        {
            var key = _configuration["Jwt:RefreshKey"];
            //If the user changes his Key, then all refresh tokens handled to who was allegedly that user are invalidated
            //key += authContext.User.Credentials.GetPasswordDelegate();

            return key;
        }
    }
}

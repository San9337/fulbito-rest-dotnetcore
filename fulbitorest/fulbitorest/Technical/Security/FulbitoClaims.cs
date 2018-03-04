using model;
using model.Enums;
using model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using model.Utils;

namespace FulbitoRest.Technical.Security
{
    public static class FulbitoClaims
    {
        public static IEnumerable<Claim> CreateClaims(User user, AuthenticationMethod authMethod)
        {
            var userCredentials = user.Credentials;
            return new List<Claim>
            {
                new Claim(Email, userCredentials.Email),
                new Claim(UniqueRandomId, Guid.NewGuid().ToString()),
                new Claim(UserId, user.Id.ToString()),
                new Claim(LoginMethod,authMethod.GetDescription())
            };
        }

        /// <summary>
        /// The id of the user in the DB
        /// </summary>
        public static string UserId => JwtRegisteredClaimNames.NameId;
        public static string Email => JwtRegisteredClaimNames.Email;
        public static string LoginMethod => "loginmethod";
        public static string UniqueRandomId => JwtRegisteredClaimNames.Jti;
    }
}

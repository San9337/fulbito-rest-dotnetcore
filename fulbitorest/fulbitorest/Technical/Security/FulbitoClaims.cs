using model;
using model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Security
{
    public static class FulbitoClaims
    {
        public static IEnumerable<Claim> CreateClaims(User user)
        {
            var userCredentials = user.Credentials;
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, userCredentials.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("LoginMethod","normal")
            };
        }


    }
}

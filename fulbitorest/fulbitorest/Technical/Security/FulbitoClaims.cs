using model;
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
        public static IEnumerable<Claim> CreateClaims(UserCredentials userCredentials)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, userCredentials.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userCredentials.User),
                new Claim("LoginMethod","normal")
            };
        }


    }
}

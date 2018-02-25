using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Security
{
    public class RefreshTokenParser
    {
        private readonly IContentSecurityHelper _helper;

        public RefreshTokenParser(IContentSecurityHelper helper)
        {
            _helper = helper;
        }

        internal string FormatToken(string refreshToken, string key)
        {
            var signature = _helper.SignRefreshToken(refreshToken, key);
            return refreshToken + "-" + signature;
        }

        /// <returns>The original refresh token</returns>
        internal string ValidateAndRetrieve(string tokenWithSignature, string key)
        {
            var split = tokenWithSignature.IndexOf("-");
            var refreshToken = tokenWithSignature.Substring(0, split);
            var signature = tokenWithSignature.Substring(split + 1, tokenWithSignature.Length - split - 1);

            if (!_helper.IsValidSignature(refreshToken, signature, key))
                throw new SecurityException("Invalide signature");

            return refreshToken;
        }
    }
}

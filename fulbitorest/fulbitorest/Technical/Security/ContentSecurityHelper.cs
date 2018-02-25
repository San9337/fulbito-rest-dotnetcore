using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Security
{
    /// <summary>
    /// Manages signing and encryption outside the web api middleware
    /// </summary>
    public interface IContentSecurityHelper
    {
        string SignRefreshToken(string token, string key);
        bool IsValidSignature(string token, string signature, string key);
    }

    public class ContentSecurityHelper : IContentSecurityHelper
    {
        public string SignRefreshToken(string refreshToken, string key)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signatureProvider = CryptoProviderFactory.Default.CreateForSigning(symmetricKey, SecurityAlgorithms.HmacSha256);

            var signatureInBytes = signatureProvider.Sign(Encoding.UTF8.GetBytes(refreshToken));

            return Convert.ToBase64String(signatureInBytes);
        }

        public bool IsValidSignature(string token, string signature, string key2)
        {
            if (signature != SignRefreshToken(token, key2))
                return false;

            return true;
        }

        /// <summary>
        /// The default encrpytpion schema for signing (e.g. tokens)
        /// </summary>
        internal static SigningCredentials GetSigninCredentials(string key)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            return creds;
        }
    }
}

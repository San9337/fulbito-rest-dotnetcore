using System;
using System.Collections.Generic;
using System.Text;

namespace model.Business
{
    public class SecurityHandler
    {
        public static string Sign(string content, string key)
        {
            var signatureProvider = CryptoProviderFactory.Default.CreateForSigning(key, SecurityAlgorithms.HmacSha256);
            return signatureProvider.Sign(Encoding.UTF8.GetBytes(content));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;

namespace model
{
    [ComplexType]
    public class Password
    {
        private static MD5 md5 = MD5.Create(); //TODO: Change to bcript or something more fun

        private string HashedPassword { get; set; }

        public Password()
        {
        }

        public Password(string password)
        {
            HashedPassword = GetHash(password);
        }

        public bool IsValid(string password)
        {
            return Matches(password, HashedPassword);
        }

        private static string GetHash(string password)
        {
            //https://msdn.microsoft.com/es-es/library/system.security.cryptography.md5(v=vs.110).aspx
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static bool Matches(string password, string hash)
        {
            string hashOfInput = GetHash(password);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

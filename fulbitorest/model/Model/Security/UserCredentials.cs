using model.Enums;
using model.Interfaces;
using model.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace model.Model.Security
{
    public class UserCredentials : IEntity
    {
        [Key, ForeignKey(nameof(User))]
        public int Id { get; set; }

        private static MD5 md5 = MD5.Create(); //TODO: Change to bcript or something more fun

        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public virtual User User { get; set; }
        public AuthenticationMethod AuthMethod { get; set; }

        public UserCredentials()
        {
        }

        public UserCredentials(string userName, string password, string email)
        {
            Email = email;

            if(!string.IsNullOrEmpty(password))
                HashedPassword = GetHash(password);

            AuthMethod = AuthenticationMethod.Fulbito;
        }
        public UserCredentials(string email)
        {
            Email = email;
            HashedPassword = null;
            AuthMethod = AuthenticationMethod.Facebook;
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return email == Email && Matches(password, HashedPassword);
        }

        public ValidationResult Validate()
        {
            if (!IsValidField(this.Email))
                return new ValidationResult("Mail is required");

            if (AuthMethod == AuthenticationMethod.Fulbito && HashedPassword == null)
                return new ValidationResult("Password is required");

            return ValidationResult.Success;
        }

        private bool IsValidField(string field)
        {
            return !string.IsNullOrEmpty(field) && !string.IsNullOrWhiteSpace(field);
        }

        public static string GetHash(string password)
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

        public static bool Matches(string password, string hash)
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

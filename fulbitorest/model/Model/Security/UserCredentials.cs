using model.Business.Structures;
using model.Enums;
using model.Exceptions;
using model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

//TODO: refactor this into an abstract class (reinforced by the UserFactory design), using hashed password as auth deleate for tokens (Conceptual bug)

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

        /// <summary>
        /// Create credentials using FULBITO schema
        /// </summary>
        public UserCredentials(FulbitoUser fulbitoUser)
        {
            Email = fulbitoUser.Email;

            if (!string.IsNullOrEmpty(fulbitoUser.Password))
                ResetPassword(fulbitoUser.Password);

            AuthMethod = AuthenticationMethod.Fulbito;
        }

        /// <summary>
        /// Create credentials using a schema OTHER than fulbito
        /// </summary>
        public UserCredentials(string email, AuthenticationMethod authMethod, string externalToken)
        {
            if (authMethod == AuthenticationMethod.Fulbito)
                throw new DevException("Wrong use of credentials constructor");

            Email = email;
            ResetPassword(externalToken);
            AuthMethod = authMethod;
        }

        public ValidationResult Validate()
        {
            if (!IsValidField(this.Email))
                return new ValidationResult("Mail is required");

            if (AuthMethod == AuthenticationMethod.Fulbito && HashedPassword == null)
                return new ValidationResult("Password is required");

            return ValidationResult.Success;
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return email == Email && Matches(password, HashedPassword);
        }

        public void ResetPassword(string newPassword)
        {
            HashedPassword = GetHash(newPassword);
        }

        /// <summary>
        /// The result of this method MUST change if the user modifies his login in any way (password, method, etc)
        /// </summary>
        public string GetPasswordDelegate()
        {
            return HashedPassword;
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

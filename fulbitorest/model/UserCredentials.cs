using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace model
{
    public class UserCredentials
    {
        /// <summary>
        /// Currently being used as "Id"
        /// </summary>
        public string User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserCredentials()
        {
        }

        public UserCredentials(string userName, string pass, string email)
        {
            User = userName;
            Password = pass;
            Email = email;
        }

        public bool AreCredentialsValid(string username, string password)
        {
            return username == User && Password == password;
        }

        public ValidationResult Validate()
        {
            if (!IsValidField(this.Email))
                return new ValidationResult("Mail is required");

            if (!IsValidField(this.Password))
                return new ValidationResult("Password is required");

            if (!IsValidField(this.User))
                return new ValidationResult("User name is required");

            return ValidationResult.Success;
        }

        private bool IsValidField(string field)
        {
            return !string.IsNullOrEmpty(field) && !string.IsNullOrWhiteSpace(field);
        }
    }
}

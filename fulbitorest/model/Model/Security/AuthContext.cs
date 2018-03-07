using model.Enums;
using model.Exceptions;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;
using System.Text;

namespace model.Model.Security
{
    public class AuthContext : IEntity
    {
        [Key, ForeignKey(nameof(User))]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public AuthenticationMethod AuthMethod { get; set; }

        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Revoked { get; set; }

        public AuthContext()
        {
        }

        public AuthContext(User user, AuthenticationMethod authMethod)
        {
            User = user;
            AuthMethod = authMethod;
        }

        public void Reset(double expirationInMinutes)
        {
            RenewToken(expirationInMinutes);
        }

        public bool IsRefreshValid(string previousToken)
        {
            return !Revoked && previousToken == RefreshToken && DateTime.Now < ExpireDate;
        }

        public void Refresh(string previousToken, double expirationInMinutes)
        {
            if (!IsRefreshValid(previousToken))
                throw new SecurityException("Invalid request, token has been revoked or is invalid");

            RenewToken(expirationInMinutes);
        }
        
        private void RenewToken(double expirationInMinutes)
        {
            if (Id == 0)
                throw new DevException("Cant create token with Id 0");
            var newToken = GenerateNewToken();

            RefreshToken = newToken;
            ExpireDate = DateTime.Now.AddMinutes(expirationInMinutes);
            Revoked = false;
        }

        private static string GenerateNewToken()
        {
            var randomString = Guid.NewGuid().ToString().Replace("-", "");
            return randomString;
        }
    }
}

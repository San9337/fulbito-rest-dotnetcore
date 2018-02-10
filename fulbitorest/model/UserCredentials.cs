using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class UserCredentials
    {
        public string User { get; set; }
        public string Password { get; set; }

        public UserCredentials()
        {
        }
        public UserCredentials(string user, string pass)
        {
            User = user;
            Password = pass;
        }

        public bool AreValid(string username, string password)
        {
            return username == User && Password == password;
        }
    }
}

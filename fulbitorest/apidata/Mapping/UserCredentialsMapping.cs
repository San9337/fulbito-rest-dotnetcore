using model;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Mapping
{
    public static class UserCredentialsMapping
    {
        public static UserCredentials Map(this UserCredentialsData data)
        {
            return new UserCredentials()
            {
                Password = data.Password,
                User = data.User
            };
        }
        public static UserCredentialsData Map(this UserCredentials data)
        {
            return new UserCredentialsData()
            {
                Password = data.Password,
                User = data.User
            };
        }
    }
}

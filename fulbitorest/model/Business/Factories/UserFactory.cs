using System;
using model.Exceptions;
using model.Model;
using model.Model.Security;
using model.Enums;
using model.Business.Structures;

namespace model.Business.Factories
{
    public static class UserFactory
    {
        public static User CreateUser(FulbitoUser fulbitoUser)
        {
            //TODO: Using nick name as user name, repeated value unless we store first name/last name
            UserCredentials newCredentials = CreateNewCredentials(fulbitoUser);

            return new User(newCredentials)
            {
                NickName = fulbitoUser.NickName,
            };
        }

        public static User CreateUser(FacebookUser fbUser)
        {
            return new User(new UserCredentials(fbUser.Email, AuthenticationMethod.Facebook, fbUser.IssuedToken))
            {
                Name = fbUser.UserName
            };
        }

        private static UserCredentials CreateNewCredentials(FulbitoUser fulbitoUser)
        {
            var newCredentials = new UserCredentials(fulbitoUser);

            var validation = newCredentials.Validate();
            if (validation != null)
                throw new UnexpectedInputException(validation.ErrorMessage);

            return newCredentials;
        }
    }
}

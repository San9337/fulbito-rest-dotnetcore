using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Factories
{
    public static class UserFactory
    {
        public static User Get()
        {
            return new User()
            {
                Age = 24,
                City = LocationFactory.GetCity(),
                Country = LocationFactory.GetCountry(),
                Credentials = new UserCredentials("Santiago","pass","san@san"),
                Gender = model.Enums.Gender.Male,
                Id = 1,
                LastName = "Gomez Cerruti",
                Name = "Santiago",
                NickName = "San",
                ProfilePictureUrl = "",
                RealTeam = TeamFactory.Get(),
                SkilledFoot = model.Enums.Foot.Right,
                State = LocationFactory.GetState()
            };
        }
    }
}

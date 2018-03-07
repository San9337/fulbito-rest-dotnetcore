using model.Business.Structures;
using model.Model;
using model.Model.Security;
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
                BirthDate = DateTime.Now.AddYears(-24),
                City = LocationFactory.GetCity(),
                Country = LocationFactory.GetCountry(),
                Credentials = new UserCredentials(new FulbitoUser() {
                    NickName = "Santiago",
                    Password = "pass",
                    Email = "san@san"
                }),
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

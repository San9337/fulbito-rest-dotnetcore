using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Contracts.Repositories;
using model.Model;
using Moq;
using testingutils.Factories;

namespace testingutils.Givens
{
    public static class UserGiven
    {
        public static User ExistingUser(Mock<IUserRepository> userRepo)
        {
            var user = UserFactory.Get();

            userRepo.Setup(ur => ur.Get(user.Id)).Returns(user);

            return user;
        }
    }
}

using datalayer.Contracts.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Providers
{
    public static class UserRepositoryMockProvider
    {
        public static Mock<IUserRepository> Get()
        {
            var mock = new Mock<IUserRepository>();
            return mock;
        }
    }
}

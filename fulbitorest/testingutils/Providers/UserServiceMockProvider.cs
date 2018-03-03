using FulbitoRest.Services;
using FulbitoRest.Services.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Providers
{
    public static class UserServiceMockProvider
    {
        public static Mock<IUserService> Get()
        {
            var mock = new Mock<IUserService>();
            return mock;
        }
    }
}

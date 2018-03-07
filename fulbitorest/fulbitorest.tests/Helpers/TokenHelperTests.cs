using FulbitoRest.Helpers.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using testingutils.Factories;
using model.Model.Security;
using Microsoft.Extensions.Configuration;
using FulbitoRest.Technical.Security;
using Moq;

namespace fulbitorest.tests.Helpers
{
    [TestClass]
    public class TokenHelperTests
    {
        private TokenHelper _sut;
        private Mock<IConfiguration> _configuration;
        private Mock<IRefreshTokenParser> _tokenParser;


        [TestInitialize]
        public void Initialize()
        {
            _configuration = new Mock<IConfiguration>();
            _tokenParser = new Mock<IRefreshTokenParser>();
            _sut = new TokenHelper(_configuration.Object, _tokenParser.Object);
        }

        private AuthContext GivenAuthContext()
        {
            var user = UserFactory.Get();
            return new AuthContext()
            {
                AuthMethod = model.Enums.AuthenticationMethod.Fulbito,
                User = user,
            };
        }
    }
}

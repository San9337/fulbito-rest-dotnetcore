using FulbitoRest.Technical.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace fulbitorest.tests.Technical.Security
{
    [TestClass]
    public class RefreshTokenParserTests
    {
        private Mock<IContentSecurityHelper> _helperMock;

        [TestInitialize]
        public void Initialize()
        {
            _helperMock = new Mock<IContentSecurityHelper>();
            _helperMock.Setup(h => h.SignRefreshToken("token", "key")).Returns("signature");
            _helperMock.Setup(h => h.IsValidSignature("token", "signature", "key")).Returns(true);
        }

        [TestMethod]
        public void TokenParser_DoesntAlterOriginalSignature()
        {
            var sut = new RefreshTokenParser(_helperMock.Object);

            var refreshToken = "token";
            var key = "key";

            var formatedToken = sut.FormatToken(refreshToken, key);
            var result = sut.ValidateAndRetrieve(formatedToken, key);

            Assert.AreEqual(refreshToken, result);
        }
    }
}

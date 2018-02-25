using FulbitoRest.Technical.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace fulbitorest.tests.Technical.Security
{
    [TestClass]
    public class ContentSecurityHelperTests
    {
        private ContentSecurityHelper _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new ContentSecurityHelper();
        }

        [TestMethod]
        public void GivenSameToken_SignRefreshToken_ReturnsTrue()
        {
            var refreshToken = "token";
            var key = "key123456789123456789";

            var signed = _sut.SignRefreshToken(refreshToken, key);
            Assert.IsTrue(_sut.IsValidSignature(refreshToken, signed, key));
        }

        [TestMethod]
        public void GivenDifferentToken_SignRefreshToken_ReturnsFalse()
        {
            var refreshToken = "token";
            var key = "key123456789123456789";

            var signed = _sut.SignRefreshToken(refreshToken, key);
            Assert.IsFalse(_sut.IsValidSignature(refreshToken+"1", signed, key));
        }
    }
}

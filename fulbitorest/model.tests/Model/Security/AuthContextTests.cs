using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Model.Security;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace model.tests.Model.Security
{
    [TestClass]
    internal class AuthContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void GivenExpiredToken_Refresh_Exception()
        {
            AuthContext sut = GivenExpiredToken();
            sut.Refresh("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void GivenRefreshAttempWithObsolete_Refresh_Exception()
        {
            AuthContext sut = GivenNonExpiredToken();
            sut.RefreshToken = "obsolete";
            sut.Refresh("updated", 1);
        }

        private static AuthContext GivenNonExpiredToken()
        {
            return new AuthContext()
            {
                ExpireDate = DateTime.Now.AddDays(1),
                Id = 123
            };
        }
        private static AuthContext GivenExpiredToken()
        {
            return new AuthContext()
            {
                ExpireDate = DateTime.Now.AddDays(-1)
            };
        }
    }
}

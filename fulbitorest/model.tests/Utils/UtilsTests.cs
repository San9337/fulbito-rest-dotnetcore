using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Enums;
using model.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace model.tests.Utils
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void GetDescription_Facebook()
        {
            var method = AuthenticationMethod.Facebook;

            Assert.AreEqual("facebook", method.GetDescription());
        }

        [TestMethod]
        public void GetDescription_Normal()
        {
            var method = AuthenticationMethod.Fulbito;

            Assert.AreEqual("normal", method.GetDescription());
        }
    }
}

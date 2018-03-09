using FulbitoRest.Helpers.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace fulbitorest.tests.Helpers
{
    [TestClass]
    public class ThirdPartyHelperTests
    {
        [TestMethod]
        public void JsonConvertTest()
        {
            var sut = new ThirdPartyHelper();
            var viewModel = sut.Deserialize("{\"id\":15}");
            Assert.AreEqual("15", viewModel.ID);
        }
    }
}

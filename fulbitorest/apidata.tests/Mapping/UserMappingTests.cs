using apidata.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Business;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using testingutils.Factories;

namespace apidata.tests.Mapping
{
    [TestClass]
    public class UserMappingTests : BaseMappingTest
    {

        [TestMethod]
        public void Map_User()
        {
            var user = UserFactory.Get();
            AssertNoNulls(user.Map());
        }

        [TestMethod]
        public void Map_User_UndefinedLocationAsNullsInData()
        {
            var user = UserFactory.Get();
            user.Location = Location.UNDEFINED;

            var data = user.Map();

            Assert.IsNotNull(data.CountryName);
        }
    }
}

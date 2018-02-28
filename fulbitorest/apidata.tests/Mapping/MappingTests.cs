using apidata.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using testingutils.Factories;

namespace apidata.tests.Mapping
{
    [TestClass]
    public class MappingTests
    {


        [TestMethod]
        public void FootMapping()
        {
            AssertIdMapped(Foot.Both.Map());
            AssertDescriptionMapped(Foot.Both.Map());
        }

        [TestMethod]
        public void GenderMapping()
        {
            AssertIdMapped(Gender.Male.Map());
            AssertDescriptionMapped(Gender.Female.Map());
        }

        [TestMethod]
        public void TeamMapping()
        {
            var team = TeamFactory.Get();
            Assert.IsNotNull(team.Map());
        }

        [TestMethod]
        public void UserMapping()
        {
            var user = UserFactory.Get();
            AssertNoNulls(user.Map());
        }

        private void AssertIdMapped<T>(T result)
        {
            var mappedId = (int) result.GetType().GetProperty("Id").GetValue(result);
            Assert.IsNotNull(mappedId);
            Assert.IsFalse(mappedId == 0);
        }
        private void AssertDescriptionMapped<T>(T result)
        {
            var mappedDesc = (string)result.GetType().GetProperty("Description").GetValue(result);
            Assert.IsFalse(string.IsNullOrEmpty(mappedDesc));
        }
        private void AssertNoNulls<T>(T result)
        {
            foreach(var property in typeof(T).GetProperties())
            {
                Assert.IsNotNull(property.GetValue(result), property.Name +" of type " + property.PropertyType.Name);
            }
        }
    }
}

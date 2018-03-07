using apidata.DataContracts;
using apidata.DataContracts.External;
using apidata.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using testingutils.Factories;
using testingutils.Mocking;

namespace apidata.tests.Mapping
{
    [TestClass]
    public class MappingTests
    {


        [TestMethod]
        public void Map_Foot()
        {
            AssertIdMapped(Foot.Both.Map());
            AssertDescriptionMapped(Foot.Both.Map());
        }

        [TestMethod]
        public void Map_Gender()
        {
            AssertIdMapped(Gender.Male.Map());
            AssertDescriptionMapped(Gender.Female.Map());
        }

        [TestMethod]
        public void Map_Team()
        {
            var team = TeamFactory.Get();
            AssertNoNulls(team.Map());
        }

        [TestMethod]
        public void Map_User()
        {
            var user = UserFactory.Get();
            AssertNoNulls(user.Map());
        }

        [TestMethod]
        public void Map_UserCredentials_FulbitoUser()
        {
            var userCredentials = Mocker.MockAllValues(new UserCredentialsData());
            var mapResult = userCredentials.MapToFulbitoUser();

            AssertNoNulls(mapResult);
            Assert.AreEqual(userCredentials.Email, mapResult.Email);
            Assert.AreEqual(userCredentials.NickName, mapResult.NickName);
            Assert.AreEqual(userCredentials.Password, mapResult.Password);
        }

        [TestMethod]
        public void Map_FacebookUser()
        {
            var fbUser = Mocker.MockAllValues(new FacebookUserViewModel());
            var mapResult = fbUser.Map("randomToken");
            AssertNoNulls(mapResult);
            Assert.AreEqual(fbUser.Email, mapResult.Email);
            Assert.AreEqual(fbUser.UserName, mapResult.UserName);
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

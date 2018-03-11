using apidata.DataContracts;
using apidata.DataContracts.External;
using apidata.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Business;
using model.Enums;
using testingutils.Factories;
using testingutils.Mocking;

namespace apidata.tests.Mapping
{
    [TestClass]
    public class MappingTests : BaseMappingTest
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
        public void Map_Match()
        {
            var match = MatchFactory.Get();
            AssertNoNulls(match.Map());
        }

        [TestMethod]
        public void Map_MatchSummary()
        {
            var match = new MatchSummary(MatchFactory.Get());
            AssertNoNulls(match.MapSummary());
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
    }
}

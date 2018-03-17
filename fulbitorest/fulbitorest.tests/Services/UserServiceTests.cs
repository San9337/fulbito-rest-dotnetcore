using datalayer.Contracts.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using testingutils.Providers;

namespace fulbitorest.tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _sut;
        private Mock<IUserRepository> _userRepo;
        private Mock<IProfessionalTeamRepository> _teamRepo;
        private Mock<ILocationService> _locationServ;

        [TestInitialize]
        public void Initialize()
        {
            _userRepo = UserRepositoryMockProvider.Get();
            _teamRepo = ProfessionalTeamRepositoryMockProvider.Get();
            _locationServ = LocationServiceMockProvider.Get();
            _sut = new UserService(_userRepo.Object, _teamRepo.Object, _locationServ.Object);
        }
    }
}

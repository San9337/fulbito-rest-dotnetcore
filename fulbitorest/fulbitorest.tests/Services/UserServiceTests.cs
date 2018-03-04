using datalayer.Contracts.Repositories;
using FulbitoRest.Services;
using FulbitoRest.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using testingutils.Factories;
using testingutils.Providers;

namespace fulbitorest.tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _sut;
        private Mock<IUserRepository> _userRepo;
        private Mock<IProfessionalTeamRepository> _teamRepo;
        private LocationService _locationServ;

        [TestInitialize]
        public void Initialize()
        {
            _userRepo = UserRepositoryMockProvider.Get();
            _teamRepo = ProfessionalTeamRepositoryMockProvider.Get();
            _sut = new UserService(_userRepo.Object, _teamRepo.Object, _locationServ);
        }
    }
}

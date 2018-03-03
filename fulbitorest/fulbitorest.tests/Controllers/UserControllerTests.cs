using fulbitorest.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Contracts.Repositories;
using FulbitoRest.Services;
using testingutils.Providers;
using FulbitoRest.Services.Contracts;
using apidata.DataContracts;
using testingutils.Givens;
using model.Model;
using Moq;
using apidata.Mapping;

namespace fulbitorest.tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _sut;
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserService> _userService;

        [TestInitialize]
        public void Initialize()
        {
            _userRepo = UserRepositoryMockProvider.Get();
            _userService = UserServiceMockProvider.Get();
            _sut = new UserController(_userRepo.Object, _userService.Object);
        }
    }
}

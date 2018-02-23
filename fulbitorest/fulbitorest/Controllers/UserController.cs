using apidata.DataContracts;
using apidata.Mapping;
using apidata.Utils;
using datalayer.Contracts.Repositories;
using FulbitoRest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using model.Enums;
using model.Model;
using model.Utils;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public UserController(
            IUserRepository userRepo, 
            UserService userService)
        {
            _userRepository = userRepo;
            _userService = userService;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public UserData Get(int id)
        {
            var user = _userRepository.Get(id);

            return user.Map();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public string Delete(int id)
        {
            _userRepository.Delete(id);

            return "user deleted";
        }

        [HttpPost]
        [Route("{id:int}")]
        public UserData Post(int id, [FromBody]UserData data)
        {
            return Put(id, data);
        }

        [HttpPut]
        [Route("{id:int}")]
        public UserData Put(int id, [FromBody]UserData data)
        {
            data.ValidateBody();

            var user = _userService.Update(id,data);

            return user.Map();
        }

        [HttpGet]
        [Route("gender/{id:int}")]
        public string GetGender(int id)
        {
            var user = _userRepository.Get(id);
            return user.Gender.GetDescription();
        }

    }
}
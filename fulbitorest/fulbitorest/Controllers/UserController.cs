using apidata.DataContracts;
using apidata.Mapping;
using apidata.Utils;
using datalayer.Contracts.Repositories;
using FulbitoRest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly LocationService _locationService;

        public UserController(IUserRepository userRepo, LocationService locationServ)
        {
            _userRepository = userRepo;
            _locationService = locationServ;
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

            var user = _userRepository.Get(id);

            user.Age = data.Age;
            user.Gender = data.Gender == "male" ? model.Enums.Gender.Male : model.Enums.Gender.Female;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.RealTeam = data.RealTeam;
            user.SkilledFoot = data.SkilledFoot;

            var location = _locationService.GetOrCreate(data.CountryName, data.StateName, data.CityName);
            user.SetLocation(location);

            _userRepository.Save(user);

            return user.Map();
        }
    }
}
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
        private readonly LocationService _locationService;
        private readonly ITeamRepository _teamRepository;

        public UserController(IUserRepository userRepo, LocationService locationServ, ITeamRepository teamRepo)
        {
            _userRepository = userRepo;
            _locationService = locationServ;
            _teamRepository = teamRepo;
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
            user.Gender = (Gender)data.GenderId;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.SkilledFoot = (Foot)data.SkilledFootId;

            if (!string.IsNullOrEmpty(data.RealTeamName))
            {
                var teamData = data.RealTeamName.Split(" - ");
                var team = _teamRepository.Get(teamData[0], teamData[1]);
                user.RealTeam = team;
            }

            var location = _locationService.GetOrCreate(data.CountryName, data.StateName, data.CityName);
            user.SetLocation(location);

            _userRepository.Save(user);

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
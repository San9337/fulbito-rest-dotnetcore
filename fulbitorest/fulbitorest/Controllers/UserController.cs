using apidata.DataContracts;
using apidata.Mapping;
using apidata.Utils;
using datalayer.Contracts.Repositories;
using FulbitoRest.Controllers;
using FulbitoRest.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace fulbitorest.Controllers
{
    /// <summary>
    /// User = Player
    /// </summary>
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(
            IUserRepository userRepo,
            IUserService userService)
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
            ValidateUserIsUsingHisEndpoint(id);
            _userRepository.Delete(id);

            return "user deleted";
        }

        [HttpPut]
        [Route("{id:int}")]
        public UserData Put(int id, [FromBody]EditProfileData data)
        {
            data.ValidateBody();
            ValidateUserIsUsingHisEndpoint(id);

            var user = _userService.Update(id, data);

            return user.Map();
        }

        [HttpPut]
        [Route("{id:int}/profilePicture")]
        public HttpResponseMessage PutProfilePicture(int id, [FromBody]ProfilePictureData data)
        {
            data.ValidateBodyNotNulls();
            ValidateUserIsUsingHisEndpoint(id);
            _userService.UpdateProfilePicture(id, data.ProfilePictureUrl);

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.RedirectMethod,
            };
            response.Headers.Add("location", "api/user/" + id);

            return response;
        }

        /// <param name="id">User id</param>
        /// <param name="types">Comma separated types</param>
        [HttpGet]
        [Route("{id:int}/stats")]
        public UserStatsData UserStats(int id, string types)
        {
            return new UserStatsData()
            {
                GamesPlayed = 0,
            };
        }
    }
}
using apidata.DataContracts;
using apidata.Mapping;
using datalayer.Contracts.Repositories;
using FulbitoRest.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/teamfan")]
    [Authorize]
    public class TeamfanController : BaseController
    {
        private readonly IProfessionalTeamRepository _teamRepository;

        public TeamfanController(IProfessionalTeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The id provided by BE</param>
        /// <returns>Id + Description</returns>
        [HttpGet]
        [Route("{id:int}")]
        public TeamData GetTeam(int id)
        {
            var team = _teamRepository.Get(id);
            return team.Map();
        }

        [HttpGet]
        [Route("")]
        public async Task<List<TeamData>> Search(string searchQuery)
        {
            var results = await _teamRepository.GetMatchingTeams(searchQuery);

            return results.Select(t => t.Map()).ToList();
        }
    }
}
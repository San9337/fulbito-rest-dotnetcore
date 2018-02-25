using apidata.DataContracts;
using apidata.Mapping;
using datalayer.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using model.Enums;
using model.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/Data")]
    [Authorize]
    public class DataController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public DataController(ITeamRepository teamRepo)
        {
            _teamRepository = teamRepo;
        }

        [HttpGet]
        [Route("gender")]
        public List<GenderData> GetGender()
        {
            var result = new List<GenderData>();
            foreach(var enumValue in EnumUtils.Values<Gender>())
            {
                result.Add(enumValue.Map());
            };
            return result;
        }

        [HttpGet]
        [Route("foot")]
        public List<FootData> GetFoot()
        {
            var result = new List<FootData>();
            foreach (var enumValue in EnumUtils.Values<Foot>())
            {
                result.Add(enumValue.Map());
            };
            return result;
        }

        [HttpGet]
        [Route("teamfan")]
        public async Task<List<TeamData>> GetTeam(string searchQuery)
        {
            var results = await _teamRepository.GetMatchingTeams(searchQuery);

            return results.Select(t => t.Map()).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FulbitoRest.Controllers;
using Microsoft.AspNetCore.Authorization;
using datalayer.Contracts.Repositories;
using apidata.Mapping;
using apidata.DataContracts;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/Team")]
    [Authorize]
    public class TeamController : BaseController
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
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
    }
}
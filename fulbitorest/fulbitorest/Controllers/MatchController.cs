using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FulbitoRest.Controllers;
using apidata.DataContracts;
using model.Model;
using apidata.Utils;
using apidata.Mapping;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/Match")]
    [Authorize]
    public class MatchController : BaseController
    {

        [HttpPost]
        [Route("")]
        public MatchData Create(MatchData data)
        {
            var match = new Match()
            {
                Owner = new model.Model.User() { Id = int.Parse(base.UserIdClaim.Value) },

                Id = 1,
                GameAddress = data.GameAddress,
                StartDateTime = DataStandards.FormatDateTime(data.StartDateTime) ?? DateTime.Now,
                DurationInMinutes = data.DurationInMinutes,
                GameFieldSize = data.GameFieldSize,
                MainPlayersTeamSize = data.MainPlayersTeamSize,
                SubstitutePlayersTeamSize = data.SubstitutePlayersTeamSize,
                RequiresApproval = data.RequiresApproval
            };

            return match.Map();
        }
    }
}
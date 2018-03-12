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
using FulbitoRest.Services.Contracts;
using System.Net.Http;

namespace fulbitorest.Controllers
{
    [Produces("application/json")]
    [Route("api/Match")]
    [Authorize]
    public class MatchController : BaseController
    {

        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchServ)
        {
            _matchService = matchServ;
        }

        [HttpPost]
        [Route("")]
        public MatchData Create([FromBody] MatchData data)
        {
            var match = _matchService.CreateMatch(data, int.Parse(base.UserIdClaim.Value));
            return match.Map();
        }
        
        [HttpGet]
        [Route("list")]
        public List<MatchSummaryData> ListRelatedMatches([FromQuery] int userId)
        {
            return _matchService.GetRelatedMatches(userId)
                .Select(ms => ms.MapSummary())
                .ToList();
        }

        [HttpPost]
        [Route("{matchId:int}/join")]
        public HttpResponseMessage JoinMatch(int matchId, [FromBody] JoinMatchData data)
        {
            data.ValidateBody();
            _matchService.JoinMatch(matchId, data);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                ReasonPhrase = "Player sucessfully added"
            };
        }

        [HttpPost]
        [Route("{matchId:int}/leave")]
        public HttpResponseMessage LeaveMatch(int matchId, [FromBody] JoinMatchData data)
        {
            data.ValidateBody();
            _matchService.LeaveMatch(matchId, data);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                ReasonPhrase = "Player sucessfully removed"
            };
        }

        [HttpPost]
        [Route("{matchId:int}/cancel")]
        public HttpResponseMessage CancelMatch(int matchId)
        {
            _matchService.CancelMatch(matchId);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                ReasonPhrase = "Match was cancelled"
            };
        }
    }
}
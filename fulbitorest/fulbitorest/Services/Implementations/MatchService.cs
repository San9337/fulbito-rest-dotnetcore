using apidata.DataContracts;
using apidata.Utils;
using datalayer.Contracts.Repositories;
using FulbitoRest.HubServices;
using FulbitoRest.Services.Contracts;
using model.Business;
using model.Exceptions;
using model.Model;
using model.Utils;
using System;
using System.Collections.Generic;

namespace FulbitoRest.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchHubService _matchHubService;
        private readonly ILocationService _locationService;


        public MatchService(
            IMatchRepository matchRepo, 
            IUserRepository userRepo,
            IMatchHubService matchHub,
            ILocationService locationServ
        )
        {
            _matchRepository = matchRepo;
            _userRepository = userRepo;
            _matchHubService = matchHub;
            _locationService = locationServ;
        }

        public Match CreateMatch(MatchData data, int ownerUserId)
        {
            var owner = _userRepository.Get(ownerUserId);
            var location = _locationService.CreateFrom(data.Location);

            var match = new Match(owner, location)
            {
                GameAddress = data.GameAddress,
                GameFieldSize = data.GameFieldSize,
                MainPlayersTeamSize = data.MainPlayersTeamSize,
                SubstitutePlayersTeamSize = data.SubstitutePlayersTeamSize,
                RequiresApproval = data.RequiresApproval,
            };

            SetDateAndDuration(data, match);

            _matchRepository.Add(match);

            return match;
        }

        public IEnumerable<MatchSummary> GetRelatedMatches(int userId)
        {
            return _matchRepository.GetSummariesForUser(userId);
        }

        private static void SetDateAndDuration(MatchData data, Match match)
        {
            var startDateTime = DataStandards.FormatDateTime(data.StartDateTime);
            if (startDateTime == DataStandards.DATE_UNDEFINED)
                throw new FulbitoException("Cant create match with undefined date");

            match.SetTime((DateTime)startDateTime, data.DurationInMinutes);
        }

        public void JoinMatch(int matchId, JoinMatchData data)
        {
            var match = _matchRepository.GetWithPlayers(matchId);

            var user = _userRepository.Get(data.PlayerId);
            var slot = AttibuteUtils.GetEnumValueFromCode(data.SlotCode);

            var newPlayer = match.AddPlayer(user, slot);
            _matchRepository.Save(match);

            _matchHubService.PlayerJoined(newPlayer);
            if (match.SlotsFree == 0)
                _matchHubService.MatchIsFull(match);
        }

        public void LeaveMatch(int matchId, JoinMatchData data)
        {
            var match = _matchRepository.GetWithPlayers(matchId);

            var removedPlayer = match.RemovePlayer(data.PlayerId);
            _matchRepository.Save(match);

            _matchHubService.PlayerLeft(removedPlayer);
        }

        public void CancelMatch(int matchId)
        {
            //Validating if it exists
            var match = _matchRepository.Get(matchId);
            _matchHubService.MatchCancelled(match);
            _matchRepository.Delete(matchId);
        }
    }
}

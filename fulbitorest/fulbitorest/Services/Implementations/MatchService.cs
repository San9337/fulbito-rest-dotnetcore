using FulbitoRest.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apidata.DataContracts;
using model.Model;
using datalayer.Contracts.Repositories;
using apidata.Utils;
using model.Exceptions;
using model.Business;
using model.Utils;

namespace FulbitoRest.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;


        public MatchService(IMatchRepository matchRepo, IUserRepository userRepo)
        {
            _matchRepository = matchRepo;
            _userRepository = userRepo;
        }

        public Match CreateMatch(MatchData data, int ownerUserId)
        {
            var owner = _userRepository.Get(ownerUserId);

            var match = new Match(owner)
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

            var newPlayer = _userRepository.Get(data.PlayerId);
            var slot = AttibuteUtils.GetEnumValueFromCode(data.SlotCode);

            match.AddPlayer(newPlayer, slot);
            _matchRepository.Save(match);
        }

        public void LeaveMatch(int matchId, JoinMatchData data)
        {
            var match = _matchRepository.GetWithPlayers(matchId);
            match.RemovePlayer(data.PlayerId);
            _matchRepository.Save(match);
        }
    }
}

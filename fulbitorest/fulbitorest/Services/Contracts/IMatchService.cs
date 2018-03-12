using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apidata.DataContracts;
using model.Model;
using model.Business;

namespace FulbitoRest.Services.Contracts
{
    public interface IMatchService : IService
    {
        Match CreateMatch(MatchData data, int ownerUserId);
        IEnumerable<MatchSummary> GetRelatedMatches(int userId);
        void JoinMatch(int matchId, JoinMatchData data);
        void LeaveMatch(int matchId, JoinMatchData data);
        void CancelMatch(int matchId);
    }
}

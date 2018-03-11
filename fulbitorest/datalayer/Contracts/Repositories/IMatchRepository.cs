using model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using model.Business;

namespace datalayer.Contracts.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        IEnumerable<MatchSummary> GetSummariesForUser(int userId);
        Match GetWithPlayers(int matchId);
    }
}

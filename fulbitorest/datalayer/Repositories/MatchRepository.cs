using datalayer.Contracts.Repositories;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using datalayer.FulbitoContext;
using model.Business;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace datalayer.Repositories
{
    public class MatchRepository : EntityFrameworkRepository<Match>, IMatchRepository
    {
        public MatchRepository(FulbitoDbContext context) : base(context)
        {
        }

        public IEnumerable<MatchSummary> GetSummariesForUser(int userId)
        {
            return FulbitoContext.Matches
                .Where(m => m.OwnerId == userId || m.MainPlayers.Any(mp => mp.UserId == userId) || m.SubstitutePlayers.Any(sp => sp.UserId == userId))
                .Include(m => m.Owner)
                .Include(m => m.Players)
                .Select(m => new MatchSummary(m));
        }

        public Match GetWithPlayers(int matchId)
        {
            var match= FulbitoContext.Matches
                .Where(m => m.Id == matchId)
                .Include(m => m.Players)
                .FirstOrDefault();
            if (match == null)
                ThrowNonExistException(matchId);

            return match;
        }
    }
}

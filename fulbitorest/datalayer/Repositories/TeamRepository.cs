using datalayer.Contracts.Repositories;
using datalayer.FulbitoContext;
using Microsoft.EntityFrameworkCore;
using model.Exceptions;
using model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datalayer.Repositories
{
    public class TeamRepository : EntityFrameworkRepository<Team>, ITeamRepository
    {
        public TeamRepository(FulbitoDbContext context) : base(context)
        {
        }

        public Team Get(string name, string countryName)
        {
            var team = FulbitoContext.Teams.First(t => t.CountryName == countryName && t.Name == name);
            if (team == null)
                throw new UnexpectedInputException("Team doesn't exist");

            return team;
        }

        public async Task<IList<Team>> GetMatchingTeams(string query)
        {
            var teamNameMatches = await FulbitoContext
                .Teams
                .Where(t => t.Name.StartsWith(query))
                .OrderBy(t => t.Name)
                .Take(5)
                .ToListAsync();

            var teamsFoundCount = teamNameMatches.Count();
            if (teamsFoundCount < 5)
            {
                var countryNameMatches = await FulbitoContext
                    .Teams
                    .Where(t => !teamNameMatches.Any(m => m.Id == t.Id) && t.CountryName.StartsWith(query))
                    .OrderBy(t => t.Name)
                    .Take(5 - teamsFoundCount)
                    .ToListAsync();
                teamNameMatches.AddRange(countryNameMatches);
            }

            return teamNameMatches;
        }
    }
}

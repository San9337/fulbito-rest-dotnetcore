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
    public class ProfessionalTeamRepository : EntityFrameworkRepository<ProfessionalTeam>, IProfessionalTeamRepository
    {
        public ProfessionalTeamRepository(FulbitoDbContext context) : base(context)
        {
        }

        public ProfessionalTeam Get(string name, string countryName)
        {
            var team = FulbitoContext.ProfessionalTeams.First(t => t.CountryName == countryName && t.Name == name);
            if (team == null)
                throw new UnexpectedInputException("Team doesn't exist");

            return team;
        }

        public ProfessionalTeam GetDefaultValue()
        {
            var @default = FulbitoContext.ProfessionalTeams.First(tf => tf.Name == ProfessionalTeam.UNDEFINED.Name);
            if (@default == null)
                throw new DevException("No default value for teams, missing migration");
            return @default;
        }

        public async Task<IList<ProfessionalTeam>> GetMatchingTeams(string query)
        {
            var teamNameMatches = await FulbitoContext
                .ProfessionalTeams
                .Where(t => t.Name.StartsWith(query) && 
                    t.Id != ProfessionalTeam.UNDEFINED.Id
                )
                .OrderBy(t => t.Name)
                .Take(5)
                .ToListAsync();

            var teamsFoundCount = teamNameMatches.Count();
            if (teamsFoundCount < 5)
            {
                var countryNameMatches = await FulbitoContext
                    .ProfessionalTeams
                    .Where(t => t.Id != ProfessionalTeam.UNDEFINED.Id && 
                        !teamNameMatches.Any(m => m.Id == t.Id) && 
                        t.CountryName.StartsWith(query)
                    )
                    .OrderBy(t => t.Name)
                    .Take(5 - teamsFoundCount)
                    .ToListAsync();
                teamNameMatches.AddRange(countryNameMatches);
            }

            return teamNameMatches;
        }
    }
}

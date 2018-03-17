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
            query = query.ToLower();

            var splits = query.Split(' ');
            if (splits.Length == 1)
            {
                return await FindBySingleSearchElement(query);
            }
            if (splits.Length > 1)
            {
                //Assuming all is team
                var results = await FindByTeamName(query);
                if (results.Any())
                    return results;

                //Assuming right most word is the country, else is team
                var country = splits[splits.Length - 1];
                var team = query.Substring(0, query.LastIndexOf(' '));

                var sqlQuery = new RawSqlString(@"
SET @team = @p0;
SET @country = @p1;

SELECT * FROM ProfessionalTeams 
WHERE Id <> 1 AND
(LOWER(Name) LIKE CONCAT('%',@team,'%')
AND LOWER(CountryName) LIKE CONCAT('%',@country,'%'))
ORDER BY Name ASC,CountryName ASC
LIMIT 5
");
                 return await FulbitoContext.ProfessionalTeams.FromSql(sqlQuery, team, country).ToListAsync();
            }

            throw new UnexpectedInputException("The query string is in a non expected format: " + query);
        }

        private async Task<IList<ProfessionalTeam>> FindBySingleSearchElement(string searchQuery)
        {
            var sqlQuery = new RawSqlString(@"
SET @search = @p0;

SELECT * FROM ProfessionalTeams 
WHERE Id <> 1 AND
(Name LIKE CONCAT('%',@search,'%')
OR CountryName LIKE CONCAT('%',@search,'%'))
ORDER BY Name ASC,CountryName ASC
LIMIT 5
");
            return await FulbitoContext.ProfessionalTeams.FromSql(sqlQuery, searchQuery).ToListAsync();
        }

        private async Task<IList<ProfessionalTeam>> FindByTeamName(string teamName)
        {
            var sqlQuery = new RawSqlString(@"
SET @search = @p0;

SELECT * FROM ProfessionalTeams 
WHERE Id <> 1 AND
LOWER(Name) LIKE CONCAT('%',@search,'%')
ORDER BY Name ASC,CountryName ASC
LIMIT 5
");
            return await FulbitoContext.ProfessionalTeams.FromSql(sqlQuery, teamName).ToListAsync();
        }
    }
}

using model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datalayer.Contracts.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team Get(string name, string countryName);

        Task<IList<Team>> GetMatchingTeams(string query);
    }
}

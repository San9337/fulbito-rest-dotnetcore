using model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datalayer.Contracts.Repositories
{
    public interface IProfessionalTeamRepository : IRepository<ProfessionalTeam>, IWithDefaultValue<ProfessionalTeam>
    {
        ProfessionalTeam Get(string name, string countryName);

        Task<IList<ProfessionalTeam>> GetMatchingTeams(string query);
    }
}

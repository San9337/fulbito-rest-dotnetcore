using model.Business;
using model.Model;

namespace datalayer.Contracts.Repositories
{
    public interface ILocationRepository : IRepository
    {
        Location SaveLocation(Location newLocation);

    }
}

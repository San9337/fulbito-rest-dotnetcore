using model.Business;
using model.Model;

namespace datalayer.Contracts.Repositories
{
    public interface ILocationRepository
    {
        Location SaveLocation(Location newLocation);

    }
}

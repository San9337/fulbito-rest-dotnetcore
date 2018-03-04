using model.Business;
using model.Model;

namespace datalayer.Contracts.Repositories
{
    public interface ILocationRepository : IRepository, IWithDefaultValue<Location>
    {
        Location SaveLocation(Location newLocation);
    }
}

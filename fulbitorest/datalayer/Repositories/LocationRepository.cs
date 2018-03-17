using datalayer.Contracts.Repositories;
using datalayer.FulbitoContext;
using model.Model;

namespace datalayer.Repositories
{
    public class LocationRepository : EntityFrameworkRepository<Location>, ILocationRepository
    {
        public LocationRepository(FulbitoDbContext context) : base(context)
        {
        }

        public Location GetDefaultValue()
        {
            return Get(Location.UNDEFINED.Id);
        }
    }
}

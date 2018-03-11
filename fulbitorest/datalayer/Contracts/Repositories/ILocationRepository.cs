using model.Business;
using model.Model;

namespace datalayer.Contracts.Repositories
{
    public interface ILocationRepository : IRepository, IWithDefaultValue<Location>
    {
        /// <summary>
        /// Creates the location, which DOESNT have any empty attributes
        /// </summary>
        Location SaveCompleteLocation(Location newLocation);

        /// <summary>
        /// Creates the locations, which may HAVE empty attributes
        /// </summary>
        Location CreateRelatedValidEntities(Location location);
    }
}

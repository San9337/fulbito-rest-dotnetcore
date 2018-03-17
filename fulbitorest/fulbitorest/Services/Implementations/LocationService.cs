using apidata.DataContracts;
using datalayer.Contracts.Repositories;
using FulbitoRest.Services.Contracts;
using model.Model;

namespace FulbitoRest.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepo)
        {
            _locationRepository = locationRepo;
        }

        public Location CreateFrom(LocationData locationData)
        {
            var location = new Location(
                description: locationData.Description,
                latitude: double.Parse(locationData.Latitude.Replace('.',',')),
                longitude: double.Parse(locationData.Longitude.Replace('.', ','))
                );

            _locationRepository.Add(location);

            return location;
        }
    }
}

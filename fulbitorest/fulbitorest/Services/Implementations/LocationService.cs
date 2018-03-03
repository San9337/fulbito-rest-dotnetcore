using datalayer.Contracts.Repositories;
using model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LocationService : IService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepo)
        {
            _locationRepository = locationRepo;
        }

        public Location GetOrCreate(string country, string state, string city)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(city))
                return Location.NonExistent;

            return _locationRepository.SaveLocation(new Location(country,state,city));
        }
    }
}

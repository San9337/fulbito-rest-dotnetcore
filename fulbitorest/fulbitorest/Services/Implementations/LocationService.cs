using datalayer.Contracts.Repositories;
using FulbitoRest.Services.Contracts;
using model.Business;
using model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepo)
        {
            _locationRepository = locationRepo;
        }

        /// <returns>A location with the "requested" related location entities</returns>
        public Location GetOrCreate(string country, string state, string city)
        {
            var location = new Location(country, state, city);
            if (location.HasCompleteSpecification)
                return _locationRepository.SaveCompleteLocation(location);

            //One or more parameters are null, create the attributes null as independent
            if (string.IsNullOrEmpty(country))
                country = Country.UNDEFINED.Name;
            if (string.IsNullOrEmpty(state))
                state = State.UNDEFINED.Name;
            if (string.IsNullOrEmpty(city))
                city = City.UNDEFINED.Name;

            return _locationRepository.CreateRelatedValidEntities(location);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using datalayer.FulbitoContext;
using datalayer.Contracts.Repositories;
using model.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using model.Business;
using model.Exceptions;

namespace datalayer.Repositories
{
    public class LocationRepository : EntityFrameworkRepository, ILocationRepository
    {
        public LocationRepository(FulbitoDbContext context) : base(context)
        {
        }

        public Location SaveCompleteLocation(Location newLocation)
        {
            if (!newLocation.HasCompleteSpecification)
                throw new DevException("Attempting to create a location with UNDEFINED attributes");

            var country = GetOrCreateCountry(newLocation.CountryName);
            var state = GetOrCreateState(newLocation.StateName, country);
            var city = GetOrCreateCity(newLocation.CityName, state, country);

            newLocation.CompleteLocation(city);

            return newLocation;
        }

        public Location CreateRelatedValidEntities(Location location)
        {
            if (location.HasCompleteSpecification)
                return SaveCompleteLocation(location);

            var undefinedLocation = GetDefaultValue();

            if (string.IsNullOrEmpty(location.CountryName))
                return undefinedLocation;
            var validCountry = GetOrCreateCountry(location.CountryName);

            if (string.IsNullOrEmpty(location.StateName))
            {
                location.CompleteLocation(undefinedLocation.City, undefinedLocation.State, validCountry);
                return location;
            }
            var validState = GetOrCreateState(location.StateName, validCountry);

            location.CompleteLocation(undefinedLocation.City, validState, validCountry);
            return location;
        }

        private Country GetOrCreateCountry(string name)
        {
            var country = FulbitoContext.Countries
                .Where(c => c.Name == name)
                .FirstOrDefault();

            if (country == null)
            {
                country = new Country() { Name = name };
                FulbitoContext.Countries.Add(country);
                FulbitoContext.SaveChanges();
            }

            return country;
        }

        private State GetOrCreateState(string name, Country country)
        {
            var state = FulbitoContext.States
                .Where(s => s.Name == name && s.Country.Name == country.Name)
                .FirstOrDefault();

            if (state == null)
            {
                state = new State() { Name = name, Country = country };
                FulbitoContext.States.Add(state);
                FulbitoContext.SaveChanges();
            }

            return state;
        }

        private City GetOrCreateCity(string name, State state, Country country)
        {
            var city = FulbitoContext.Cities
                .Where(c => c.Name == name && c.State.Name == state.Name && c.State.Country.Name == country.Name)
                .FirstOrDefault();

            if (city == null)
            {
                city = new City() { Name = name, State = state };
                FulbitoContext.Cities.Add(city);
                FulbitoContext.SaveChanges();
            }

            return city;
        }

        public Location GetDefaultValue()
        {
            var defaultCity = FulbitoContext.Cities
                .Where(c => c.Id == Location.UNDEFINED.City.Id)
                .Include(c => c.State)
                .Include(c => c.State.Country)
                .First();

            return new Location(defaultCity);
        }
    }
}

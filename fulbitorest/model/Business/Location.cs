using System;
using model.Exceptions;
using model.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace model.Business
{
    /// <summary>
    /// To ease the management of location related entities
    /// </summary>
    public class Location
    {
        public static Location UNDEFINED => new Location(City.UNDEFINED);

        public Location(City city)
        {
            this.CompleteLocation(city);
        }

        public Country Country { get; private set; }
        public State State { get; private set; }
        public City City { get; private set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public bool HasCompleteSpecification => !string.IsNullOrEmpty(CountryName) && !string.IsNullOrEmpty(StateName) && !string.IsNullOrEmpty(CityName);
        [NotMapped]
        public bool IsCompletelyDefined => !City.IsUndefined() && !State.IsUndefined() && !Country.IsUndefined();

        public Location(string country, string state, string city)
        {
            CountryName = country;
            StateName = state;
            CityName = city;
        }

        public void CompleteLocation(City city)
        {
            CompleteLocation(city, city.State, city.State.Country);
        }

        public void CompleteLocation(City city, State state, Country country)
        {
            City = city ?? throw new DevException("Null parameters in location");
            State = state ?? throw new DevException("Null parameters in location");
            Country = country ?? throw new DevException("Null parameters in location");

            CountryName = Country.Name;
            StateName = State.Name;
            CityName = city.Name;
        }
    }
}

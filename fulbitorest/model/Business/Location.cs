using model.Exceptions;
using model.Model;

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

        public Location(string country, string state, string city)
        {
            if (country == null || state == null || city == null)
                throw new DevException("Locations can't have undefined attributes");

            CountryName = country;
            StateName = state;
            CityName = city;
        }

        /// <summary>
        /// Preformed by the repository
        /// </summary>
        public void CompleteLocation(City city)
        {
            City = city ?? throw new DevException("Null parameters in location");
            State = city.State ?? throw new DevException("Null parameters in location");
            Country = State.Country ?? throw new DevException("Null parameters in location");

            CountryName = Country.Name;
            StateName = State.Name;
            CityName = city.Name;
        }
    }
}

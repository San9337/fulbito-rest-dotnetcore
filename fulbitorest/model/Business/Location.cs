using model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Business
{
    /// <summary>
    /// To ease the management of location related entities
    /// </summary>
    public class Location
    {
        public Location(string country, string state, string city)
        {
            CountryName = country;
            StateName = state;
            CityName = city;
        }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        /// <summary>
        /// Preformed by the repository
        /// </summary>
        public void CompleteLocation(Country country, State state, City city)
        {
            Country = country;
            State = state;
            City = city;
        }

        public Country Country { get; private set; }
        public State State { get; private set; }
        public City City { get; private set; }
    }
}

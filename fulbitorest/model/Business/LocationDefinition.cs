using System;
using System.Collections.Generic;
using System.Text;

namespace model.Business
{
    public class LocationDefinition
    {
        public string CountryName { get; set; }
        public string StateName { get; set; }

        public bool IsComplete => !string.IsNullOrEmpty(CountryName) && !string.IsNullOrEmpty(StateName);

        public LocationDefinition(string country, string state)
        {
            CountryName = country;
            StateName = state;
        }
    }
}

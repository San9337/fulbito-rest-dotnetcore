using model.Business;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Factories
{
    public static class LocationFactory
    {
        public static Location Get()
        {
            var location = new Location("Argentina","Buenos Aires","Atte Brown"); 
            location.CompleteLocation(GetCity());
            return location;
        }

        public static Country GetCountry() => new Country() { Id = 1, Name = "Argentina" };
        public static State GetState() => new State() { Id = 1, Name = "Buenos Aires", Country = GetCountry() };
        public static City GetCity() => new City() { Id = 1, Name = "Atte Brown", State = GetState() };
    }
}

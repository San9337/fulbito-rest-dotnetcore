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
            var location = new Location("desc", 30, 30);
            return location;
        }

        public static Country GetCountry() => new Country() { Id = 1, Name = "Argentina" };
    }
}

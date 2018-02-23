using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Factories
{
    public static class TeamFactory
    {
        public static Team Get()
        {
            return new Team()
            {
                CountryName = "Argentina",
                Id = 1,
                Name = "San Lorenzo"
            };
        }
    }
}

using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Factories
{
    public static class TeamFactory
    {
        public static ProfessionalTeam Get()
        {
            return new ProfessionalTeam()
            {
                CountryName = "Argentina",
                Id = 1,
                Name = "San Lorenzo",
                LogoUrl = "logoUrl"
            };
        }
    }
}

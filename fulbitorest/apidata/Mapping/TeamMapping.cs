using apidata.DataContracts;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Mapping
{
    public static class TeamMapping
    {
        public static TeamData Map(this Team team)
        {
            return new TeamData()
            {
                Id = team.Id,
                Name = team.Name,
                CountryName = team.CountryName
            };
        }
    }
}

using apidata.DataContracts;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using apidata.Mapping;
using apidata.Utils;

namespace apidata.Mapping
{
    public static class UserMapping
    {
        public static UserData Map(this User user)
        {
            var data = user.MapTo<UserData>();

            data.CountryName = user.Country?.Name;
            data.StateName = user.State?.Name;
            data.CityName = user.City?.Name;

            data.TeamFanId = user.RealTeam?.Id;
            data.Foot = user.SkilledFoot.Map();
            data.Gender = user.Gender.Map();

            data.BirthDate = DataStandards.FormatDate(user.BirthDate);

            return data;
        }

        public static User Map(this UserData data)
        {
            var user = data.MapTo<User>();

            return user;
        }
    }
}

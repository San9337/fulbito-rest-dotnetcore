using apidata.DataContracts;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using apidata.Mapping;

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

            if(user.RealTeam != null)
            {
                data.RealTeamId = user.RealTeam.Id;
                data.RealTeamName = user.RealTeam.FormattedName;
            }
            

            return data;
        }

        public static User Map(this UserData data)
        {
            var user = data.MapTo<User>();

            return user;
        }
    }
}

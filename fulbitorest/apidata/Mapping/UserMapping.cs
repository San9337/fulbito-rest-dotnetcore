using apidata.DataContracts;
using apidata.Utils;
using model.Model;

namespace apidata.Mapping
{
    public static class UserMapping
    {
        public static UserData Map(this User user)
        {
            var data = user.MapTo<UserData>();

            MapLocation(user, data);

            data.TeamFanId = user.RealTeam?.Id;
            data.Foot = user.SkilledFoot.Map();
            data.Gender = user.Gender.Map();

            data.BirthDate = DataStandards.FormatDate(user.BirthDate);

            return data;
        }

        private static void MapLocation(User user, UserData data)
        {
            if (!user.Country.IsUndefined())
                data.CountryName = user.Country.Name;
        }

        public static User Map(this UserData data)
        {
            var user = data.MapTo<User>();

            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apidata.DataContracts;
using model.Model;
using datalayer.Contracts.Repositories;
using model.Enums;
using FulbitoRest.Services.Contracts;
using apidata.Utils;

namespace FulbitoRest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfessionalTeamRepository _teamRepository;
        private readonly LocationService _locationService;

        public UserService(IUserRepository userRepository, IProfessionalTeamRepository teamRepository, LocationService locationService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _locationService = locationService;
        }

        public User Update(int id, UserData data)
        {
            var user = _userRepository.Get(id);

            UpdateBasicData(data, user);
            UpdateTeam(data.TeamFanId, user);
            UpdateLocation(data, user);

            _userRepository.Save(user);

            return user;
        }

        private static void UpdateBasicData(UserData data, User user)
        {
            user.BirthDate = DataStandards.FormatDate(data.BirthDate);
            user.Gender = (Gender)data.Gender.Id;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.SkilledFoot = (Foot)data.Foot.Id;
        }

        private void UpdateLocation(UserData data, User user)
        {
            var location = _locationService.GetOrCreate(data.CountryName, data.StateName, data.CityName);
            user.SetLocation(location);
        }

        internal void UpdateTeam(int? teamFanId, User user)
        {
            var team = teamFanId != null ?
                            _teamRepository.Get(teamFanId ?? 0) :
                            _teamRepository.GetDefaultValue();

            user.RealTeam = team;
        }
    }
}

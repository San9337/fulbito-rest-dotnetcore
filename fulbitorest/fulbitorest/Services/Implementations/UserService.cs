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
        private readonly ITeamRepository _teamRepository;
        private readonly LocationService _locationService;

        public UserService(IUserRepository userRepository, ITeamRepository teamRepository, LocationService locationService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _locationService = locationService;
        }

        public User Update(int id, UserData data)
        {
            var user = _userRepository.Get(id);

            user.BirthDate = DataStandards.FormatDate(data.BirthDate);
            user.Gender = (Gender)data.Gender.Id;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.SkilledFoot = (Foot)data.Foot.Id;

            if (data.TeamFanId != null)
            {
                var team = _teamRepository.Get(data.TeamFanId ?? 0);
                user.RealTeam = team;
            }

            var location = _locationService.GetOrCreate(data.CountryName, data.StateName, data.CityName);
            user.SetLocation(location);

            _userRepository.Save(user);

            return user;
        }
    }
}

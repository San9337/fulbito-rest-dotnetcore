using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apidata.DataContracts;
using model.Model;
using datalayer.Contracts.Repositories;
using model.Enums;

namespace FulbitoRest.Services
{
    public class UserService : IService
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

        internal User Update(int id, UserData data)
        {
            var user = _userRepository.Get(id);

            user.Age = data.Age;
            user.Gender = (Gender)data.GenderId;
            user.ProfilePictureUrl = data.ProfilePictureUrl;
            user.SkilledFoot = (Foot)data.Foot.Id;

            if (data.RealTeamId != null)
            {
                var team = _teamRepository.Get(data.RealTeamId ?? 0);
                user.RealTeam = team;
            }

            var location = _locationService.GetOrCreate(data.CountryName, data.StateName, data.CityName);
            user.SetLocation(location);

            _userRepository.Save(user);

            return user;
        }
    }
}

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
        private readonly ILocationService _locationService;

        public UserService(IUserRepository userRepository, IProfessionalTeamRepository teamRepository, ILocationService locationService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _locationService = locationService;
        }

        public User Update(int id, EditProfileData data)
        {
            var user = _userRepository.Get(id);

            UpdateBasicData(data, user);
            UpdateTeam(data.TeamFanId, user);
            UpdateLocation(data, user);

            _userRepository.Save(user);

            return user;
        }

        private static void UpdateBasicData(EditProfileData data, User user)
        {
            user.BirthDate = DataStandards.FormatDate(data.BirthDate);
            user.Gender = (Gender)data.Gender.Id;
            user.SkilledFoot = (Foot)data.Foot.Id;
            user.NickName = data.NickName;
        }

        private void UpdateLocation(EditProfileData data, User user)
        {
            var location = _locationService.CreateFrom(data.Location);
            user.Location = location;
        }

        internal void UpdateTeam(int teamFanId, User user)
        {
            if(teamFanId != user.RealTeamId)
            {
                user.RealTeam = _teamRepository.Get(teamFanId);
            }
        }

        public void UpdateProfilePicture(int id, string profilePictureUrl)
        {
            var user = _userRepository.Get(id);
            user.ProfilePictureUrl = profilePictureUrl;
            _userRepository.Save(user);
        }
    }
}

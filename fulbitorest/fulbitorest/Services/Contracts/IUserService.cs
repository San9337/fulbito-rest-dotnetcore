using apidata.DataContracts;
using model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services.Contracts
{
    public interface IUserService : IService
    {
        User Update(int id, EditProfileData data);
        void UpdateProfilePicture(int id, string profilePictureUrl);
    }
}

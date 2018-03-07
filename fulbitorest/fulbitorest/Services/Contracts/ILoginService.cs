using model.Business.Structures;
using model.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services.Contracts
{
    public interface ILoginService : IService
    {
        AuthContext Register(FulbitoUser fulbitoUser);
        AuthContext Login(string email, string password);
        AuthContext RefreshToken(string currentRefreshToken);
        Task<AuthContext> LoginWithFacebook(string fbToken);
    }
}

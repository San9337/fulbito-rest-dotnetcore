using model.Business.Structures;
using System.Threading.Tasks;

namespace FulbitoRest.Helpers.Contracts
{
    public interface IThirdPartyHelper : IHelper
    {
        Task<FacebookUser> GetFacebookUser(string fbToken);
    }
}

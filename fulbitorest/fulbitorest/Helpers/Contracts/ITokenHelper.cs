using model.Enums;
using model.Model;
using model.Model.Security;
using System.Security.Claims;

namespace FulbitoRest.Helpers.Contracts
{
    public interface ITokenHelper : IHelper
    {
        AccessTokenLogin GenerateJwtAccessToken(User user, AuthenticationMethod method);
        string CreateRefreshToken(AuthContext authContext);
        string ValidateAndRemoveSignature(string tokenWithSignature);
    }

    public class AccessTokenLogin
    {
        public string AccessToken { get; internal set; }
        public ClaimsPrincipal ClaimsPrincipal { get; internal set; }
    }
}

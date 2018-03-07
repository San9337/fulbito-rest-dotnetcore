using datalayer.Contracts.Repositories;
using FulbitoRest.Helpers.Contracts;
using FulbitoRest.Services.Contracts;
using model.Business.Factories;
using model.Business.Structures;
using model.Enums;
using model.Exceptions;
using model.Model;
using model.Model.Security;
using System.Security;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LoginService : ILoginService
    {
        private readonly double refreshTokenExpiration = 5;

        private readonly IUserRepository _userRepository;
        private readonly IAuthContextRepository _authRepository;
        private readonly IThirdPartyHelper _thirdPartyHelper;
        private readonly ITokenHelper _tokenHelper;

        public LoginService(
            IUserRepository userRepo, 
            IAuthContextRepository authRepo, 
            IThirdPartyHelper thirdPartyHelp,
            ITokenHelper tokenHelper
        )
        {
            _userRepository = userRepo;
            _authRepository = authRepo;
            _thirdPartyHelper = thirdPartyHelp;
            _tokenHelper = tokenHelper;
        }

        public AuthContext Login(string email, string password)
        {
            var user = _userRepository.GetUserForCredentials(email, password);
            return ResetToken(user, AuthenticationMethod.Fulbito);
        }

        public AuthContext Register(FulbitoUser fulbitoUser)
        {
            if (_userRepository.AlreadyExists(fulbitoUser.Email))
                throw new UnexpectedInputException(nameof(UserCredentials.Email), "Email already exists");
            
            var newUser = UserFactory.CreateUser(fulbitoUser);
            _userRepository.Add(newUser);

            return ResetToken(newUser, AuthenticationMethod.Fulbito);
        }

        public async Task<AuthContext> LoginWithFacebook(string fbToken)
        {
            FacebookUser fbUser = await _thirdPartyHelper.GetFacebookUser(fbToken);

            var email = fbUser.Email;
            if (email == null)
                throw new FulbitoException("Facebook didnt send the user's email");

            if (!_userRepository.AlreadyExists(email))
            {
                var newUser = UserFactory.CreateUser(fbUser);
                _userRepository.Add(newUser);
            }

            var user = _userRepository.GetByEmail(email);

            return ResetToken(user, AuthenticationMethod.Facebook);
        }

        internal AuthContext ResetToken(User user, AuthenticationMethod authMethod)
        {
            if(!_authRepository.Exists(user.Id))
                _authRepository.Add(new AuthContext(user, authMethod));
            
            var auth = _authRepository.Get(user.Id);
            auth.Reset(refreshTokenExpiration);

            _authRepository.Save(auth);

            return auth;
        }

        public AuthContext RefreshToken(string token)
        {
            //Remove the signature
            var currentRefreshToken = _tokenHelper.ValidateAndRemoveSignature(token);
            var authContext = _authRepository.GetAuthContext(currentRefreshToken);

            if (!authContext.IsRefreshValid(currentRefreshToken))
            {
                authContext.Revoked = true;
                _authRepository.Save(authContext);
                throw new SecurityException("Token has been revoked");
            }

            authContext.Refresh(currentRefreshToken, refreshTokenExpiration);
            _authRepository.Save(authContext);

            return authContext;
        }
    }
}

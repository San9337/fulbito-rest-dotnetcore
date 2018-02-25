using datalayer.Contracts.Repositories;
using model.Enums;
using model.Exceptions;
using model.Model;
using model.Model.Security;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LoginService : IService
    {
        private readonly double refreshTokenExpiration = 5;

        private readonly IUserRepository _userRepository;
        private readonly IAuthContextRepository _authRepository;

        public LoginService(IUserRepository userRepo, IAuthContextRepository authRepo)
        {
            _userRepository = userRepo;
            _authRepository = authRepo;
        }

        internal AuthContext Login(string email, string password)
        {
            var user = _userRepository.GetUserForCredentials(email, password);
            return ResetToken(user, AuthenticationMethod.Fulbito);
        }

        internal AuthContext Register(string nickName, string email, string password)
        {
            if (_userRepository.AlreadyExists(email))
                throw new UnexpectedInputException(nameof(UserCredentials.Email), "Email already exists");

            var newCredentials = new UserCredentials(nickName,password,email);

            var validation = newCredentials.Validate();
            if (validation != null)
                throw new UnexpectedInputException(validation.ErrorMessage);

            var newUser = new User(newCredentials)
            {
                NickName = nickName,
            };

            _userRepository.Add(newUser);

            return ResetToken(newUser, AuthenticationMethod.Fulbito);
        }

        internal async Task<AuthContext> LoginWithFacebook(string fbToken)
        {
            var path = "https://graph.facebook.com/me?access_token=" + fbToken;
            var uri = new Uri(path);

            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                var contentError = await response.Content.ReadAsStringAsync();
                throw new FulbitoException("Facebook rejected the request (" + path + ")\n" + contentError);
            }

            var content = await response.Content.ReadAsStringAsync();
            var fbUser = JsonConvert.DeserializeObject<FacebookUserViewModel>(content);

            var email = fbUser.Email;
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

        internal AuthContext RefreshToken(string currentRefreshToken)
        {
            //TODO: Validate identity inside refresh token
            var authContext = _authRepository.GetAuthContext(currentRefreshToken);
            
            if(!authContext.IsRefreshValid(currentRefreshToken))
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

    public class FacebookUserViewModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

using datalayer.Contracts.Repositories;
using datalayer.FulbitoContext;
using Microsoft.EntityFrameworkCore;
using model;
using model.Exceptions;
using model.Model;
using System.Linq;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        internal User Login(string email, string password)
        {
            return _userRepository.GetUserForCredentials(email, password);
        }

        internal User Register(string nickName, string email, string password)
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

            return newUser;
        }

        internal async Task<User> LoginWithFacebook(string fbToken)
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
            return _userRepository.GetByEmail(email);
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

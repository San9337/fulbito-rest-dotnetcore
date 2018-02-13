using apidata;
using datalayer.Contracts;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LoginService
    {
        private IRepository<UserCredentials> _credentialsRepository;
        private IList<UserCredentials> Credentials => _credentialsRepository.All().ToList();

        public LoginService(IRepository<UserCredentials> credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public UserCredentials Login(string username, string password)
        {
            //Look in table or something
            return Credentials.FirstOrDefault(c => c.AreCredentialsValid(username, password));
        }

        internal UserCredentials Register(UserCredentials credentials)
        {
            if (Credentials.Any(c => c.User == credentials.User))
                return null;

            var newUser = new UserCredentials(credentials.User, credentials.Password, credentials.Email);
            _credentialsRepository.Add(newUser);

            return newUser;
        }
    }
}

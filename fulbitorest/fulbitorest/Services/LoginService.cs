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
            return Credentials.FirstOrDefault(c => c.AreValid(username, password));
        }

        internal UserCredentials Register(string username, string password)
        {
            if (Credentials.Any(c => c.User == username))
                return null;

            var newUser = new UserCredentials(username, password);
            _credentialsRepository.Add(newUser);

            return newUser;
        }
    }
}

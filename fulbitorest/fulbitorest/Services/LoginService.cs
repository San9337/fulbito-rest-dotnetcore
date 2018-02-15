using apidata;
using datalayer.Contracts;
using FulbitoRest.Exceptions;
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
            var validation = credentials.Validate();

            if(validation != null)
                throw new UnexpectedInputException(validation.ErrorMessage);
            
            if (Credentials.Any(c => c.User == credentials.User))
                throw new UnexpectedInputException(nameof(UserCredentials.User), " user name already exists");

            var newUser = new UserCredentials(credentials.User, credentials.Password, credentials.Email);
            _credentialsRepository.Add(newUser);

            return newUser;
        }
    }
}

using datalayer.Contracts.Repositories;
using datalayer.FulbitoContext;
using Microsoft.EntityFrameworkCore;
using model;
using model.Exceptions;
using model.Model;
using System.Linq;

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


    }
}

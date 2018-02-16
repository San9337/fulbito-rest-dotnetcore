using apidata;
using datalayer.Contracts;
using FulbitoRest.Exceptions;
using FulbitoRest.Repositories;
using Microsoft.EntityFrameworkCore;
using model;
using model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services
{
    public class LoginService
    {
        private readonly FulbitoDbContext _context;

        public LoginService(FulbitoDbContext context)
        {
            _context = context;
        }

        public User Login(string email, string password)
        {
            //Look in table or something
            //return _context.Users.FirstOrDefault(u => u.Credentials.AreCredentialsValid(email, password));
            return _context.UserCredentials
                .Where(c => c.HashedPassword == UserCredentials.GetHash(password))
                .Include(c => c.User)
                .FirstOrDefault()
                .User;
        }

        internal User Register(string nickName, string email, string password)
        {
            if (UserAlreadyExists(email))
                throw new UnexpectedInputException(nameof(UserCredentials.Email), "Email already exists");

            var newCredentials = new UserCredentials(nickName,password,email);
            var validation = newCredentials.Validate();

            if (validation != null)
                throw new UnexpectedInputException(validation.ErrorMessage);

            var newUser = new User()
            {
                Credentials = newCredentials,
                NickName = nickName,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        private bool UserAlreadyExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}

using datalayer.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using model.Model;
using datalayer.FulbitoContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using model.Exceptions;
using model;

namespace datalayer.Repositories
{
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(FulbitoDbContext context) : base(context)
        {
        }

        public override User Get(int id)
        {
            var user = FulbitoContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.Credentials)
                .Include(u => u.Country)
                .Include(u => u.State)
                .Include(u => u.City)
                .FirstOrDefault();

            if (user == null)
                throw new UnexpectedInputException("User doesn't exist");

            return base.Get(id);
        }

        public bool AlreadyExists(string email)
        {
            return FulbitoContext.UserCredentials.Any(c => c.Email == email);
        }

        public User GetUserForCredentials(string email, string password)
        {
            var matchingCredentialsId = FulbitoContext.UserCredentials
                .Where(c => c.Email == email && c.HashedPassword == UserCredentials.GetHash(password))
                .Select(c => c.Id);

            if (!matchingCredentialsId.Any())
                return null;

            return Get(matchingCredentialsId.First());
        }
    }
}

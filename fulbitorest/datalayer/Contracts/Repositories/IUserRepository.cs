using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool AlreadyExists(string email);
        User GetUserForCredentials(string email, string password);
        User GetByEmail(string email);
    }
}

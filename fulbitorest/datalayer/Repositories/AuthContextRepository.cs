using datalayer.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using datalayer.FulbitoContext;
using model.Model.Security;
using System.Linq;
using System.Security;
using Microsoft.EntityFrameworkCore;

namespace datalayer.Repositories
{
    public class AuthContextRepository : EntityFrameworkRepository<AuthContext>, IAuthContextRepository
    {
        public AuthContextRepository(FulbitoDbContext context) : base(context)
        {
        }

        public AuthContext GetAuthContext(string refreshToken)
        {
            var authMatch = FulbitoContext.AuthContexts
                .Where(c => c.RefreshToken == refreshToken)
                .Include(a => a.User)
                .Include(a => a.User.Credentials)
                .FirstOrDefault();
            if (authMatch == null)
                throw new SecurityException("Received non existent refresh token");

            return authMatch;
        }
    }
}

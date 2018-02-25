using datalayer.Contracts.Repositories;
using model.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;
using datalayer.FulbitoContext;

namespace datalayer.Repositories
{
    public class TokenRepository : EntityFrameworkRepository, ITokenRepository
    {
        public TokenRepository(FulbitoDbContext context) : base(context)
        {
        }
        

    }
}

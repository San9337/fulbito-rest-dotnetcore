using model.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace datalayer.Contracts.Repositories
{
    public interface IAuthContextRepository : IRepository<AuthContext>
    {
        AuthContext GetAuthContext(string refreshToken);
    }
}

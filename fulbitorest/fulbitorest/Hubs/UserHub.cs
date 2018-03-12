using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Hubs
{
    public class UserHub : BaseHub<IUserClient>
    {

        public async Task Register(int userId)
        {
            //TODO: try to move this to the onConnected and rely on authentication to set this relationship
            await Groups.AddAsync(Context.ConnectionId, GroupName.ForUser(userId));
        }
    }

    public interface IUserClient
    {

    }

}

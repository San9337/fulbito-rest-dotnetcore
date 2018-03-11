using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace notifications.HubServices
{
    public class HubService<THub> where THub : Hub
    {

        private IHubContext<THub> _hubContext;

        public HubService(IHubContext<THub> hubContext)
        {
            _hubContext = hubContext;
        }
    }
}

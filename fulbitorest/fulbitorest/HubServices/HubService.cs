using Microsoft.AspNetCore.SignalR;
using model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FulbitoRest.HubServices
{
    public class HubService<THub, TClient> where THub : Hub
    {
        private IHubContext<THub> _hubContext;

        protected IHubClients Clients => _hubContext.Clients;
        protected IGroupManager Groups => _hubContext.Groups;

        public HubService(IHubContext<THub> hubContext)
        {
            if (hubContext == null)
                throw new DevException("Cannot DI a Hub: " + nameof(THub));
            _hubContext = hubContext;

            if (Clients == null)
                throw new DevException("Hub client interface binding not working");
        }
    }
}

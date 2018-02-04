using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Hubs
{
    public class NotificationTestHub : Hub
    {
        public Task Test(string message)
        {
            return Task
                .Delay(3000)
                .ContinueWith((t) => Clients.All.InvokeAsync("receive", DateTime.Now.ToString("hh:mm:ss") +":"+ message));
        }
    }
}

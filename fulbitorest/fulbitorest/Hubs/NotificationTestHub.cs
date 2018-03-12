using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace FulbitoRest.Hubs
{
    /// <summary>
    /// For testing notifications
    /// </summary>
    public class NotificationTestHub : Hub
    {
        private readonly ICustomLogger _logger;
        public NotificationTestHub(ICustomLogger logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.Log("Connection");

            //https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
            var connectionId = base.Context.Connection.ConnectionId;
            var user = base.Context.Connection.User;

            return base.OnConnectedAsync();
        }

        public Task Test(string message)
        {
            var req = Context.Connection.GetHttpContext().Request;

            _logger.Log("Message received from client " + message);
            return Task
                .Delay(3000)
                .ContinueWith((t) => Clients.All.InvokeAsync("receive", DateTime.Now.ToString("hh:mm:ss") +":"+ message));
        }
    }
}

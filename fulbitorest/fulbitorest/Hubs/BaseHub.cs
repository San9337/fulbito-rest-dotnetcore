using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using model.Exceptions;
using model.Interfaces;
using model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Hubs
{
    //https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-server
    //Reporting Progress from hub method invocations

    //https://stackoverflow.com/questions/20724511/signalr-sending-parameter-to-onconnected -> Como pasar parametros en el onConnected

    public class BaseHub<T> : Hub<T> where T : class
    {
        //When a Hub method executes synchronously and the transport is WebSocket, 
        //subsequent invocations of methods on the Hub from the same client are blocked until the Hub method completes.
        public override Task OnConnectedAsync()
        {
            //Each client connecting to a hub passes a unique connection id
            var connectionId = base.Context.ConnectionId;


            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }

    public static class HubGroup
    {
        public static string ForMatch(int matchId)
        {
            return "m-" + matchId;
        }
        
        public static string ForUser(int userId)
        {
            return "u-" + userId;
        }
    }
}

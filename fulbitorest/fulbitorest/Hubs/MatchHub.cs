using FulbitoRest.Technical.Interception;
using FulbitoRest.Technical.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Hubs
{
    public class MatchHub : BaseHub<IMatchRoomClient>
    {
        private readonly ICustomLogger _logger;
        public MatchHub(ICustomLogger logger)
        {
            _logger = logger;
        }

        public async Task JoinAsOwner(int matchId)
        {
            await Groups.AddAsync(Context.ConnectionId, GroupName.ForMatchOwner(matchId));
        }

        public async Task JoinAsPlayer(int matchId)
        {
            await Groups.AddAsync(Context.ConnectionId, GroupName.ForMatchPlayer(matchId));
        }
    }

    public interface IMatchRoomClient : IMatchOwnerClient, IMatchPlayerClient
    {
    }

    public interface IMatchOwnerClient
    {
        void UserJoined(string message);
        void UserLeft(string message);
    }
    public interface IMatchPlayerClient
    {
        void MatchIsFull(string message);
        void MatchCancelled(string message);
    }
}



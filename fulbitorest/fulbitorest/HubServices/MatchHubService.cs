using FulbitoRest.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using model.Model;

namespace FulbitoRest.HubServices
{
    public class MatchHubService : HubService<MatchHub, IMatchClient>, IMatchHubService
    {
        public MatchHubService(IHubContext<MatchHub> hubContext) 
            : base(hubContext)
        {
        }

        public void PlayerJoined(Match match, Player player)
        {
            Clients.Group(HubGroup.ForUser(match.OwnerId)).UserJoined(player.User.Name + "(" + player.UserId + ")" + " joined match: " + match.Id);
        }

        public void MatchIsFull(Match match)
        {
            Clients.Group(HubGroup.ForMatch(match.Id)).MatchIsFull("Match is full " + match.Id);
        }
    }

    public interface IMatchHubService
    {
        void PlayerJoined(Match match, Player player);
    }
}

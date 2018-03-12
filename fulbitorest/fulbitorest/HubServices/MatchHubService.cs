using FulbitoRest.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using model.Model;
using FulbitoRest.HubServices.Contracts;

namespace FulbitoRest.HubServices
{
    public class MatchHubService : HubService<MatchHub, IMatchRoomClient>, IMatchHubService
    {
        public MatchHubService(IHubContext<MatchHub> hubContext) 
            : base(hubContext)
        {
        }

        public void PlayerJoined(Player player)
        {
            var matchId = player.MatchId;
            var msg = "Player with Id (" + player.UserId + ")" + " joined match: " + matchId;
            Clients.Group(GroupName.ForMatchOwner(matchId)).InvokeAsync(nameof(IMatchRoomClient.UserJoined),msg);
        }

        public void PlayerLeft(Player player)
        {
            var matchId = player.MatchId;
            var msg = "Player with Id (" + player.UserId + ")" + " left match: " + matchId;
            Clients.Group(GroupName.ForMatchOwner(matchId)).InvokeAsync(nameof(IMatchRoomClient.UserLeft), msg);
        }

        public void MatchIsFull(Match match)
        {
            var msg = "Match is full " + match.Id;
            Clients.Group(GroupName.ForMatchPlayer(match.Id)).InvokeAsync(nameof(IMatchRoomClient.MatchIsFull),msg);
            Clients.Group(GroupName.ForMatchOwner(match.Id)).InvokeAsync(nameof(IMatchRoomClient.MatchIsFull), msg);
        }

        public void MatchCancelled(Match match)
        {
            //IMPORTANT: assuming only the owner can cancel a match
            var msg = "Cancelled Match: " + match.Id;
            Clients.Group(GroupName.ForMatchPlayer(match.Id)).InvokeAsync(nameof(IMatchRoomClient.MatchCancelled), msg);
        }
    }

    public interface IMatchHubService : IHubService
    {
        void PlayerJoined(Player player);
        void PlayerLeft(Player player);
        void MatchIsFull(Match match);
        void MatchCancelled(Match match);
    }
}

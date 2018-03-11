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
    public class MatchHub : BaseHub<IMatchClient>
    {
        private readonly ICustomLogger _logger;
        public MatchHub(ICustomLogger logger)
        {
            _logger = logger;
        }

        public async Task<string> ListenMatch(int matchId) 
        {
            //Assumming the one listening is the match owner
            //userRepo
            //matchRepo
            //crear el room
            //agregar user al room
            await Groups.AddAsync(Context.ConnectionId, HubGroup.ForMatch(matchId));
            //room => match, usuarios, metodo para contactar usuarios
            
            return "OK";
        }
    }

    public interface IMatchClient
    {
        void UserJoined(string message);
        void MatchIsFull(string message);
    }
}

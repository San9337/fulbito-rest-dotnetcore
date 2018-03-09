using model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace model.Model
{
    public class Match : IEntity
    {
        public int Id { get; set; }

        public User Owner { get; set; }

        public string GameAddress { get; set; }
        public DateTime StartDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int GameFieldSize { get; set; }
        public int MainPlayersTeamSize { get; set; }
        public int SubstitutePlayersTeamSize { get; set; }
        public bool RequiresApproval { get; set; }
    }
}

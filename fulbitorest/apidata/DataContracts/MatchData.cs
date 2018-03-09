using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class MatchData
    {
        [DataMember(Name = "ownerId")]
        public int OwnerId { get; set; }
        [DataMember(Name = "gameAddress")]
        public string GameAddress { get; set; }
        [DataMember(Name = "startDateTime")]
        public string StartDateTime { get; set; }
        [DataMember(Name = "durationInMinutes")]
        public int DurationInMinutes { get; set; }
        [DataMember(Name = "gameFieldSize")]
        public int GameFieldSize { get; set; }
        [DataMember(Name ="mainPlayersTeamSize")]
        public int MainPlayersTeamSize { get; set; }
        [DataMember(Name = "substitutePlayersTeamSize")]
        public int SubstitutePlayersTeamSize { get; set; }
        [DataMember(Name = "requiresApproval")]
        public bool RequiresApproval { get; set; }
    }
}

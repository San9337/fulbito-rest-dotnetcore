using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class UserStatsData
    {
        [DataMember(Name ="gamesplayed")]
        public int GamesPlayed { get; set; }
    }
}

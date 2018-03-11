using model.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class JoinMatchData
    {
        [DataMember(Name = "playerId")]
        public int PlayerId { get; set; }
        [DataMember(Name ="slotCode")]
        public string SlotCode { get; set; }
    }
}

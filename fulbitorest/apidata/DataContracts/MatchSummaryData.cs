using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class MatchSummaryData
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }
        [DataMember(Name ="ownerId")]
        public int OwnerId { get; set; }
        [DataMember(Name ="address")]
        public string Address { get; set; }
        [DataMember(Name ="startDateTime")]
        public string StartDateTime { get; set; }
        [DataMember(Name = "duration")]
        public int DurationInMinutes { get; set; }
        [DataMember(Name = "slotsFree")]
        public int SlotsFree { get; set; }
        [DataMember(Name ="slotsTotal")]
        public int SlotsTotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using model.Model;

namespace model.Business
{
    /// <summary>
    /// Has all the match's relevant data
    /// </summary>
    public class MatchSummary
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Address { get; set; }
        public DateTime StartDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int SlotsFree { get; set; }
        public int SlotsTotal { get; set; }
        public MatchSummary(Match m)
        {
            Id = m.Id;
            OwnerId = m.OwnerId;
            Address = m.GameAddress;
            StartDateTime = m.StartDateTime;
            DurationInMinutes = m.DurationInMinutes;
            SlotsTotal = m.SlotsTotal;
            SlotsFree = m.SlotsFree;
        }
    }
}

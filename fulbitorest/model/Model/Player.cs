using model.Enums;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Model
{
    public class Player : IEntity
    {
        public int Id { get; set; }

        public SlotEnum Slot { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Match))]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

        protected Player()
        {
        }

        public Player(Match match, User user, SlotEnum slot)
        {
            Match = match;
            MatchId = match.Id;
            User = user;
            UserId = user.Id;
            Slot = slot;
        }
    }
}

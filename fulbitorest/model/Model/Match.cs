using model.Exceptions;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using model.Enums;

namespace model.Model
{
    public class Match : IEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public string GameAddress { get; set; }
        public DateTime StartDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime EndDateTime { get; set; }
        public int GameFieldSize { get; set; }
        public int MainPlayersTeamSize { get; set; }
        public int SubstitutePlayersTeamSize { get; set; }
        public bool RequiresApproval { get; set; }

        public IList<Player> Players { get; set; }

        [NotMapped]
        public IEnumerable<Player> MainPlayers => Players.Where(p => p.Slot == SlotEnum.Team_A_Main || p.Slot == SlotEnum.Team_A_Subs);
        [NotMapped]
        public IEnumerable<Player> SubstitutePlayers => Players.Where(p => p.Slot == SlotEnum.Team_B_Main || p.Slot == SlotEnum.Team_B_Subs);
        [NotMapped]
        public int SlotsTotal => (MainPlayersTeamSize + SubstitutePlayersTeamSize) * 2;
        [NotMapped]
        public int SlotsFree => SlotsTotal - Players.Count;

        protected Match()
        {
        }

        public Match(User owner)
        {
            Owner = owner;
            OwnerId = owner.Id;

            Players = new List<Player>();
        }

        public void SetTime(DateTime startDateTime, int durationInMinutes)
        {
            StartDateTime = startDateTime;
            DurationInMinutes = durationInMinutes;
            EndDateTime = StartDateTime.AddMinutes(durationInMinutes);
        }

        public void AddPlayer(User newPlayer, SlotEnum slot)
        {
            if (Players.Any(p => p.UserId == newPlayer.Id))
                throw new FulbitoException("Player is already present on that team");

            switch (slot)
            {
                case SlotEnum.Team_A_Main:
                case SlotEnum.Team_A_Subs:
                    ValidatePlayersArraySize(MainPlayers, MainPlayersTeamSize);
                    break;
                case SlotEnum.Team_B_Main:
                case SlotEnum.Team_B_Subs:
                    ValidatePlayersArraySize(SubstitutePlayers, SubstitutePlayersTeamSize);
                    break;
                case SlotEnum.UNDEFINED:
                    throw new FulbitoException("No definimos como usar un join undefined");
            }
            Players.Add(new Player(this, newPlayer, slot));
        }

        public void RemovePlayer(int userId)
        {
            var removedPlayer = Players.Where(p => p.UserId == userId).FirstOrDefault();
            if (removedPlayer == null)
                throw new FulbitoException("Player doesnt exists in this match");
            Players.Remove(removedPlayer);
        }

        private void ValidatePlayersArraySize(IEnumerable<Player> playerArray, int limit)
        {
            if (playerArray.Count() >= limit)
                throw new FulbitoException("Not enough free slots for another player");
        }
    }
}

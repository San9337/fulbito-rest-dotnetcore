using model.Enums;
using model.Exceptions;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace model.Model
{
    public class Match : IEntity
    {
        public static readonly int MIN_DURATION_IN_MINUTES = 10;

        public int Id { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public string GameAddress { get; set; }
        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public DateTime StartDateTime { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime EndDateTime { get; set; }
        public int GameFieldSize { get; set; }
        public bool RequiresApproval { get; set; }

        /// <summary>
        /// Main players per team
        /// </summary>
        public int MainPlayersTeamSize { get; set; }
        /// <summary>
        /// Substitute players per team
        /// </summary>
        public int SubstitutePlayersTeamSize { get; set; }

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

        public Match(User owner, Location location)
        {
            Owner = owner;
            OwnerId = owner.Id;
            Location = location;
            LocationId = location.Id;

            Players = new List<Player>();
        }

        public void SetTime(DateTime startDateTime, int durationInMinutes)
        {
            if (startDateTime < DateTime.Now)
                throw new ValidationException(Errors.Match_InvalidDate);
            if (durationInMinutes < MIN_DURATION_IN_MINUTES)
                throw new ValidationException(Errors.Match_InvalidDuration);

            StartDateTime = startDateTime;
            DurationInMinutes = durationInMinutes;
            EndDateTime = StartDateTime.AddMinutes(durationInMinutes);
        }

        public Player AddPlayer(User user, SlotEnum slot)
        {
            if (Players.Any(p => p.UserId == user.Id))
                throw new FulbitoException("Player is already present on that team");

            switch (slot)
            {
                case SlotEnum.Team_A_Main:
                case SlotEnum.Team_A_Subs:
                    ValidatePlayersArraySize(MainPlayers, MainPlayersTeamSize + SubstitutePlayersTeamSize);
                    break;
                case SlotEnum.Team_B_Main:
                case SlotEnum.Team_B_Subs:
                    ValidatePlayersArraySize(SubstitutePlayers, SubstitutePlayersTeamSize + SubstitutePlayersTeamSize);
                    break;
                case SlotEnum.UNDEFINED:
                    throw new FulbitoException("No definimos como usar un join undefined");
            }

            var newPlayer = new Player(this, user, slot);
            Players.Add(newPlayer);

            return newPlayer;
        }

        public Player RemovePlayer(int userId)
        {
            var removedPlayer = Players.Where(p => p.UserId == userId).FirstOrDefault();
            if (removedPlayer == null)
                throw new FulbitoException("Player doesnt exists in this match");
            Players.Remove(removedPlayer);

            return removedPlayer;
        }

        private void ValidatePlayersArraySize(IEnumerable<Player> playerArray, int limit)
        {
            if (playerArray.Count() >= limit)
                throw new FulbitoException("Not enough free slots for another player");
        }
    }
}

using model.Business;
using model.Enums;
using model.Exceptions;
using model.Interfaces;
using model.Model.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace model.Model
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public DateTime? BirthDate { get; set; }
        public Foot SkilledFoot { get; set; }
        public Gender Gender { get; set; }

        public virtual UserCredentials Credentials { get; set; }
        public string Email => Credentials.Email;

        [ForeignKey(nameof(RealTeam))]
        public int RealTeamId { get; set; }
        public virtual ProfessionalTeam RealTeam { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public User()
        {
        }

        public User(UserCredentials credentials)
        {
            Credentials = credentials ?? throw new DevException("Creating user with null credentials");

            credentials.User = this;
        }
    }
}

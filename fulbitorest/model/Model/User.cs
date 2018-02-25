using model.Enums;
using model.Exceptions;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using model.Business;
using model.Model.Security;

namespace model.Model
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public string ProfilePictureUrl { get; set; }

        public Foot SkilledFoot { get; set; }
        public Gender Gender { get; set; }

        public virtual UserCredentials Credentials { get; set; }
        public string Email => Credentials.Email;

        public virtual Team RealTeam { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }

        public User()
        {
        }
        public User(UserCredentials credentials)
        {
            Credentials = credentials ?? throw new DevException("Creating user with null credentials");

            credentials.User = this;
        }

        public void SetLocation(Location location)
        {
            Country = location.Country;
            State = location.State;
            City = location.City;
        }
    }
}

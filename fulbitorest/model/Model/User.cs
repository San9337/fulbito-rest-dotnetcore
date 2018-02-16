using model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string SkilledFoot { get; set; }

        public Gender Gender { get; set; }

        public virtual UserCredentials Credentials { get; set; }
        public string Email => Credentials.Email;

        public string RealTeam { get; set; }
        public string LocationName { get; set; }
        public string CountryName { get; set; }
    }
}

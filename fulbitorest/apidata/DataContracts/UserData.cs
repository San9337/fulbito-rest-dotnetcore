﻿using System.Runtime.Serialization;

namespace apidata.DataContracts
{
    [DataContract]
    public class UserData
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name ="email")]
        public string Email { get; set; }

        [DataMember(Name = "nickName")]
        public string NickName { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "urlProfilePicture")]
        public string ProfilePictureUrl { get; set; }

        [DataMember(Name = "teamFan")]
        public string RealTeam { get; set; }

        [DataMember(Name = "location")]
        public string LocationName { get; set; }

        [DataMember(Name = "country")]
        public string CountryName { get; set; }
        
        [DataMember(Name = "foot")]
        public string SkilledFoot { get; set; }
    }
}
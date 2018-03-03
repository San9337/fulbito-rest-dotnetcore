using System;
using System.Runtime.Serialization;

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

        [DataMember(Name = "birthdate")]
        public string BirthDate { get; set; }

        [DataMember(Name = "gender")]
        public GenderData Gender { get; set; }

        [DataMember(Name = "urlProfilePicture")]
        public string ProfilePictureUrl { get; set; }

        [DataMember(Name = "teamFanId")]
        public int? TeamFanId { get; set; }

        [DataMember(Name = "foot")]
        public FootData Foot { get; set; }

        [DataMember(Name = "country")]
        public string CountryName { get; set; }
        [DataMember(Name = "state")]
        public string StateName { get; set; }
        [DataMember(Name = "city")]
        public string CityName { get; set; }
    }
}

using System.Runtime.Serialization;

namespace apidata.DataContracts
{
    [DataContract]
    public class EditProfileData
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "nickName")]
        public string NickName { get; set; }

        [DataMember(Name = "birthdate")]
        public string BirthDate { get; set; }

        [DataMember(Name = "gender")]
        public GenderData Gender { get; set; }

        [DataMember(Name = "teamFanId")]
        public int TeamFanId { get; set; }

        [DataMember(Name = "foot")]
        public FootData Foot { get; set; }

        [DataMember(Name = "location")]
        public LocationData Location { get; set; }
    }
}

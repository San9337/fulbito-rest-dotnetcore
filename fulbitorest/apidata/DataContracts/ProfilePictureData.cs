using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class ProfilePictureData
    {
        [DataMember(Name = "urlProfilePicture")]
        public string ProfilePictureUrl { get; set; }
    }
}

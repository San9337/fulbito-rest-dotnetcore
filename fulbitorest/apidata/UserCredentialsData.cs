using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata
{
    [DataContract]
    public class UserCredentialsData
    {
        [DataMember(Name ="nickname")]
        public string User { get; set; }

        [DataMember(Name ="password")]
        public string Password { get; set; }

        [DataMember(Name ="email")]
        public string Email { get; set; }
    }
}

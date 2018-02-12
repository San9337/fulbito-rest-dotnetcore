using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata
{
    [DataContract]
    public class UserCredentialsData
    {
        [DataMember(Name ="user")]
        public string User { get; set; }

        [DataMember(Name ="password")]
        public string Password { get; set; }
    }
}

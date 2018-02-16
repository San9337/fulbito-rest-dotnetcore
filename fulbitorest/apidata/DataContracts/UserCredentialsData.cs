using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class UserCredentialsData
    {
        [DataMember(Name ="nickname")]
        public string NickName { get; set; }

        [DataMember(Name ="password")]
        public string Password { get; set; }

        [DataMember(Name ="email")]
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts.External
{
    [DataContract]
    public class FacebookUserViewModel
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        [DataMember(Name = "name")]
        public string UserName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}

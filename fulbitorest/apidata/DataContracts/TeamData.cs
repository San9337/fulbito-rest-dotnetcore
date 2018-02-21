using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class TeamData
    {
        [DataMember(Name = "codeName")]
        public string NameAndCountry { get; set; }
    }
}

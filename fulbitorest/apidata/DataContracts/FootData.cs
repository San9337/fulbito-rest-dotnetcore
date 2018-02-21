using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class FootData
    {
        [DataMember(Name ="description")]
        public string Description { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}

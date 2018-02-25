using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class RefreshRequestData
    {
        [DataMember(Name = "refreshtoken")]
        public string SignedRefreshToken { get; set; }
    }
}

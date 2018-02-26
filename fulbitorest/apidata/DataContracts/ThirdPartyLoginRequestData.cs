using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.DataContracts
{
    [DataContract]
    public class ThirdPartyLoginRequestData
    {
        [DataMember(Name ="token")]
        public string Token { get; set; }
    }
}

using System.Runtime.Serialization;

namespace apidata.Responses
{
    [DataContract]
    public class TokenResponseData
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }
    }
}

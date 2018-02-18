using System.Runtime.Serialization;

namespace apidata.Responses
{
    [DataContract]
    public class TokenResponseData
    {
        public static TokenResponseData Failure => new TokenResponseData()
        {
            Token = "",
            IsSuccess = false
        };

        public static TokenResponseData Success(string token) => new TokenResponseData()
        {
            Token = token,
            IsSuccess = true
        };

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }
    }
}

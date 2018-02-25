using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace apidata.Responses
{
    [DataContract]
    public class RefreshTokenResponseData
    {
        public static RefreshTokenResponseData Failure (string reason)=> new RefreshTokenResponseData()
        {
            AccessToken = "",
            RefreshToken = "",
            IsSuccess = false,
            Reason = reason,
        };

        public static RefreshTokenResponseData Success(string access, string refresh) => new RefreshTokenResponseData()
        {
            RefreshToken = refresh,
            AccessToken = access,
            IsSuccess = true
        };

        [DataMember(Name = "refreshtoken")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "accesstoken")]
        public string AccessToken { get; set; }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }
    }
}

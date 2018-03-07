using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.DataContracts.External
{
    public class FacebookUserViewModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("name")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

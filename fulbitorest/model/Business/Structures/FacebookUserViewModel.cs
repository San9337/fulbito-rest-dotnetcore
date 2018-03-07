using System;
using System.Collections.Generic;
using System.Text;

namespace model.Business.Structures
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
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

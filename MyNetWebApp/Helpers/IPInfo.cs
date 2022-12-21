using System;
using Newtonsoft.Json;

namespace MyNetWebApp.Helpers
{
    public class IPInfo
    {
        [JsonProperty("ip")]
        public string IP { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}


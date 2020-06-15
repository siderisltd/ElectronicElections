using Newtonsoft.Json;

namespace ElectronicElections.Infrastructure.Models
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}

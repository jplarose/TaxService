using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class  Jurisdictions
    {
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("county")]
        public string County { get; set; } = string.Empty;

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;
    }
}

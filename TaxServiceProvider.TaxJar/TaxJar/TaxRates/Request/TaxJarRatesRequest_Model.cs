using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class TaxJarRatesRequest_Model
    {
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        [JsonProperty("zip")]
        public string ZIP { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        [JsonProperty("street")]
        public string Street { get; set; } = string.Empty;
    }
}

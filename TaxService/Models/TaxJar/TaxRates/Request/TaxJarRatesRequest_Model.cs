using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaxService.Models.TaxJar
{
    public class TaxJarRatesRequest_Model
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string ZIP { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }
}

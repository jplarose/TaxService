using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class TaxJarLineItems
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("quantity")]
        public string? Quantity { get; set; }

        [JsonProperty("product_tax_code")]
        public string? ProductTaxCode { get; set; }

        [JsonProperty("unit_price")]
        public string? UnitPrice { get; set; }

        [JsonProperty("discount")]
        public string? Discount { get; set; }
    }
}

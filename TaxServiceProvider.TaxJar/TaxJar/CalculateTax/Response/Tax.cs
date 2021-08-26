using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class Tax
    {
        [JsonProperty("order_total_amount")]
        public decimal OrderTotalAmount { get; set; }

        [JsonProperty("shipping")]
        public decimal Shipping { get; set; }

        [JsonProperty("taxable_amount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("amount_to_collect")]
        public decimal AmountToCollect { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("tax_source")]
        public string TaxSource { get; set; } = string.Empty;

        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; } = string.Empty;

        [JsonProperty("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; } = new Jurisdictions();

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; } = new Breakdown();
    }
}

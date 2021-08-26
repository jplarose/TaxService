using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class LineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("taxable_amount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public decimal TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public decimal CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public decimal StateTaxableAmount { get; set; }

        [JsonProperty("state_sales_tax_rate")]
        public decimal StateSalesTaxRate { get; set; }

        [JsonProperty("state_amount")]
        public decimal StateAmount { get; set; }

        [JsonProperty("county_taxable_amount")]
        public decimal CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public decimal CountyTaxRate { get; set; }

        [JsonProperty("county_amount")]
        public decimal CountyAmount { get; set; }

        [JsonProperty("city_taxable_amount")]
        public decimal CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public decimal CityTaxRate { get; set; }

        [JsonProperty("city_amount")]
        public decimal CityAmount { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public decimal SpecialTaxRate { get; set; }

        [JsonProperty("special_district_amount")]
        public decimal SpecialDistrictAmount { get; set; }
    }
}

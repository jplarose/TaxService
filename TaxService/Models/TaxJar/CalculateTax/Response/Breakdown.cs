using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaxService.Models.TaxJar
{
    public class Breakdown
    {
        [JsonProperty("taxable_amount")]
        public int TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public int StateTaxableAmount { get; set; }

        [JsonProperty("state_tax_rate")]
        public double StateTaxRate { get; set; }

        [JsonProperty("state_tax_collectable")]
        public double StateTaxCollectable { get; set; }

        [JsonProperty("county_taxable_amount")]
        public int CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonProperty("county_tax_collectable")]
        public double CountyTaxCollectable { get; set; }

        [JsonProperty("city_taxable_amount")]
        public int CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public int CityTaxRate { get; set; }

        [JsonProperty("city_tax_collectable")]
        public int CityTaxCollectable { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public int SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonProperty("special_district_tax_collectable")]
        public double SpecialDistrictTaxCollectable { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaxService.Models.TaxJar
{
    public class LineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("taxable_amount")]
        public int TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public int StateTaxableAmount { get; set; }

        [JsonProperty("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonProperty("state_amount")]
        public double StateAmount { get; set; }

        [JsonProperty("county_taxable_amount")]
        public int CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonProperty("county_amount")]
        public double CountyAmount { get; set; }

        [JsonProperty("city_taxable_amount")]
        public int CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public int CityTaxRate { get; set; }

        [JsonProperty("city_amount")]
        public int CityAmount { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public int SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonProperty("special_district_amount")]
        public double SpecialDistrictAmount { get; set; }
    }
}

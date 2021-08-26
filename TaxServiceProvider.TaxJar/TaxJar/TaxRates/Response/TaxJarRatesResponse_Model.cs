using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class TaxJarRatesResponse_Model
    {
        [JsonProperty("rate")]
        public RatesResponseAttributes Rate { get; set; } = new RatesResponseAttributes();
    }

    public class RatesResponseAttributes
    {
        // Common Attributes

        [JsonProperty("zip")]
        public string Zip { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("state_rate")]
        public decimal StateRate { get; set; }

        [JsonProperty("county")]
        public string County { get; set; } = string.Empty;

        [JsonProperty("county_rate")]
        public decimal CountyRate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        [JsonProperty("country_rate")]
        public decimal CountryRate { get; set; }

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        [JsonProperty("city_rate")]
        public decimal CityRate { get; set; }

        [JsonProperty("combined_district_rate")]
        public decimal CombinedDistrictRate { get; set; }

        [JsonProperty("combined_rate")]
        public decimal CombinedRate { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        // European Union Specific

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("standard_rate")]
        public decimal StandardRate { get; set; }

        [JsonProperty("reduced_rate")]
        public decimal ReducedRate { get; set; }

        [JsonProperty("super_reduced_rate")]
        public decimal SuperReducedRate { get; set; }

        [JsonProperty("parking_rate")]
        public decimal ParkingRate { get; set; }

        [JsonProperty("distance_sale_threshold")]
        public decimal DistanceSaleThreshold { get; set; }
    }
}

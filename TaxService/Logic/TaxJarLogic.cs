using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TaxService.Models.TaxJar;

namespace TaxService.Logic
{
    public class TaxJarLogic
    {
        private RestClient client;

        public TaxJarLogic()
        {
            // Create the RestSharp client with the Authorization as a default header for all requests
            // Hard coding here, in real situation there would be a global config in the consuming application where these are stored
            client = new RestClient("https://api.taxjar.com/v2");
            client.AddDefaultHeader("Authorization", $"Bearer 5da2f821eee4035db4771edab942a4cc");
            client.UseNewtonsoftJson();
        }

        public async Task<decimal> CalculateTax (TaxJarCalculateTax_Model taxJarCalculateTax_Model)
        {
            string jsonBody = JsonConvert.SerializeObject(taxJarCalculateTax_Model);

            var request = new RestRequest("/taxes", Method.POST);
            request.AddJsonBody(jsonBody);

            var response = await client.PostAsync<TaxJarCalculateTaxResponse_Model>(request);

            
            return response.Tax.AmountToCollect;
        }

        public async Task<decimal> GetLocationTaxRates(TaxJarRatesRequest_Model taxJarRatesRequest_Model)
        {
            var request = new RestRequest($"/rates/{taxJarRatesRequest_Model.ZIP}", Method.GET);

            // Build out the rest of the URL if the optional Params are included
            LocationTaxRatesURLBuilder(taxJarRatesRequest_Model, request);

            var response = await client.GetAsync<TaxJarRatesResponse_Model>(request);

            return response.Rate.CombinedRate;
        }

        // Method to add the additional optional parameters to the GET request
        private void LocationTaxRatesURLBuilder (TaxJarRatesRequest_Model taxJarRatesRequest_Model, RestRequest request)
        {
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.Country))
            {
                request.AddParameter("country", taxJarRatesRequest_Model.Country);
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.City))
            {
                request.AddParameter("city", taxJarRatesRequest_Model.City);
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.State))
            {
                request.AddParameter("state", taxJarRatesRequest_Model.State);
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.Street))
            {
                request.AddParameter("street", taxJarRatesRequest_Model.Street);
            }
        }
    }
}

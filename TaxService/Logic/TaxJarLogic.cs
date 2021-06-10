using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TaxService.Models.TaxJar;

namespace TaxService.Logic
{
    public class TaxJarLogic
    {
        private RestClient client;

        public TaxJarLogic()
        {
            // Create the RestSharp client with the Authorization as a default header for all requests
            // Hard coding here, in real situation there would be a global config where these are stored
            client = new RestClient("https://api.taxjar.com/v2");
            client.AddDefaultHeader("Authorization", $"Bearer 5da2f821eee4035db4771edab942a4cc");
        }

        public async Task<decimal> CalculateTax (TaxJarCalculateTax_Model taxJarCalculateTax_Model)
        {
            string jsonBody = JsonConvert.SerializeObject(taxJarCalculateTax_Model);

            var request = new RestRequest("/taxes", Method.POST);
            request.AddJsonBody(jsonBody);

            var response = await client.PostAsync<TaxJarCalculateTaxResponse_Model>(request);

            
            return response.Tax.AmountToCollect;
        }

        public async Task<decimal> GetLocationTaxRates()
        {
            return 1;
        }
    }
}

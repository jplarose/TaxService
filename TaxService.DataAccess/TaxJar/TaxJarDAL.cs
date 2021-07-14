using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxService.Models.TaxJar;

namespace TaxService.DataAccess.TaxJar
{
    public class TaxJarDAL : ITaxJarDAL
    {
        private RestClient client;

        /// <summary>
        /// Create the RestSharp client with the Authorization as a default header for all requests
        /// Hard coding here, in real situation there would be a global config in the consuming application where these are stored
        /// </summary>
        public TaxJarDAL()
        {
            client = new RestClient("https://api.taxjar.com/v2");
            client.AddDefaultHeader("Authorization", "Bearer 5da2f821eee4035db4771edab942a4cc");
        }

        public async Task<TaxJarCalculateTaxResponse_Model> CalculateTax(TaxJarCalculateTax_Model taxJarCalculateTax_Model)
        {
            string jsonBody = JsonConvert.SerializeObject(taxJarCalculateTax_Model);

            var request = new RestRequest("/taxes", Method.POST);
            request.AddJsonBody(jsonBody);

            return await client.PostAsync<TaxJarCalculateTaxResponse_Model>(request);
        }

        public async Task<TaxJarRatesResponse_Model> GetLocationTaxRates(string ratesRequestURLString)
        {
            var request = new RestRequest($"/rates/{ratesRequestURLString}", Method.GET);

            return await client.GetAsync<TaxJarRatesResponse_Model>(request);
        }
    }
}

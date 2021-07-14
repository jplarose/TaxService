using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TaxService.Models.TaxJar;
using TaxService.DataAccess.TaxJar;

namespace TaxService.Logic
{
    public class TaxJarLogic
    {
        private RestClient client;
        private ITaxJarDAL _taxJarDAL;

        /// <summary>
        /// Create the RestSharp client with the Authorization as a default header for all requests
        /// Hard coding here, in real situation there would be a global config in the consuming application where these are stored
        /// </summary>
        public TaxJarLogic(ITaxJarDAL taxJarDAL)
        {
            _taxJarDAL = taxJarDAL;
        }

        public async Task<decimal> CalculateTax (TaxJarCalculateTax_Model taxJarCalculateTax_Model)
        {
            var response = await _taxJarDAL.CalculateTax(taxJarCalculateTax_Model);
            
            return response.Tax.AmountToCollect;
        }

        public async Task<decimal> GetLocationTaxRates(TaxJarRatesRequest_Model taxJarRatesRequest_Model)
        {
            var request = new RestRequest($"/rates/{taxJarRatesRequest_Model.ZIP}", Method.GET);

            // Build out the rest of the URL if the optional Params are included -- Still do this here in the Logic class
            LocationTaxRatesURLBuilder(taxJarRatesRequest_Model, request);

            var response = await client.GetAsync<TaxJarRatesResponse_Model>(request);

            return response.Rate.CombinedRate;
        }

        
        /// <summary>
        /// Method to add the additional optional parameters to the GET request
        /// </summary>
        /// <param name="taxJarRatesRequest_Model"></param>
        /// <param name="request"></param>
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

using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TaxService.Models.TaxJar;
using TaxService.DataAccess.TaxJar;
using System.Text;
using System.Collections.Generic;

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
            // Build out the URL if the optional Params are included
            string populatedURL = LocationTaxRatesURLBuilder(taxJarRatesRequest_Model);

            var response = await _taxJarDAL.GetLocationTaxRates(populatedURL);
                                
            return response.Rate.CombinedRate;
        }

        
        /// <summary>
        /// Method to add the additional optional parameters to the GET request URL
        /// </summary>
        /// <param name="taxJarRatesRequest_Model"></param>
        private string LocationTaxRatesURLBuilder (TaxJarRatesRequest_Model taxJarRatesRequest_Model)
        {
            StringBuilder urlBuilder = new StringBuilder();
            List<string> parametersToAdd = new List<string>();

            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.Country))
            {
                parametersToAdd.Add($"country={taxJarRatesRequest_Model.Country}");               
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.City))
            {
                parametersToAdd.Add($"city={taxJarRatesRequest_Model.City}");
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.State))
            {
                parametersToAdd.Add($"state={taxJarRatesRequest_Model.State}");
            }
            if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.Street))
            {
                parametersToAdd.Add($"street={taxJarRatesRequest_Model.Street}");
            }

            urlBuilder.Append((parametersToAdd.Count > 0) ? $"{taxJarRatesRequest_Model.ZIP}?" : taxJarRatesRequest_Model.ZIP);

            for (int index = 0; index < parametersToAdd.Count; index++ )
            {
                urlBuilder.Append(index > 0 ? "&" : string.Empty);

                urlBuilder.Append(parametersToAdd[index]);
            }

            return urlBuilder.ToString();
        }
    }
}

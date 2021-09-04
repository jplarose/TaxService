using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar.Models;
using Serilog;
using TaxService.Exceptions;

namespace TaxServiceProvider.TaxJar
{
    public class TaxJarServiceProvider: ITaxServiceProvider
    {
        private readonly RestClient client;
        private readonly CancellationTokenSource cancellationTokenSource;

        public TaxJarServiceProvider()
        {
            client = new RestClient("https://api.taxjar.com/v2");
            client.AddDefaultHeader("Authorization", "Bearer 5da2f821eee4035db4771edab942a4cc");

            cancellationTokenSource = new CancellationTokenSource();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.EventLog("TaxJarService")
                .CreateLogger();
        }


        public async Task<decimal> CalculateTax(CalculateTaxRequest request)
        {
            decimal taxToCollect;
            TaxJarCalculateTax_Model taxJarCalculateTax_Model = new TaxJarCalculateTax_Model
            {
                Amount = request.SaleAmount,
                Shipping = request.Shipping,
                ToStreet = request.Customer.StreetAddress,
                ToCity = request.Customer.City,
                ToState = request.Customer.State,
                ToZip = request.Customer.ZipCode,
                ToCountry = request.Customer.Country,
                FromZip = request.FromZipCode,
                FromState = request.FromState
            };

            string jsonBody = JsonConvert.SerializeObject(taxJarCalculateTax_Model);

            var restRequest = new RestRequest("/taxes", Method.POST);
            restRequest.AddJsonBody(jsonBody);
            
            try
            {
                cancellationTokenSource.CancelAfter(100000);
                var response = await client.ExecuteAsync(restRequest, Method.POST, cancellationTokenSource.Token);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TaxJarCalculateTaxResponse_Model responseObject = JsonConvert.DeserializeObject<TaxJarCalculateTaxResponse_Model>(response.Content);
                    return responseObject.Tax.AmountToCollect;
                }
                else
                {
                    Log.Error($"TaxService did return a successful status. Status: {response.StatusCode} - {response.StatusDescription}");
                    throw new TaxServiceException("");
                }
            }
            catch (TaskCanceledException ex)
            {
                Log.Error($"TaskCanceledException thrown due to timeout, message: {ex.Message}, Inner Exception: {ex.InnerException}, Stack Trace: {ex.StackTrace}");
                throw new TimeoutException();
            }
        }

        public async Task<decimal> GetLocationTaxes(GetLocationTaxRateRequest request)
        {
            // populate the URL if the optional Params are included
            string populatedURL = LocationTaxRatesURLBuilder(request);

            var restRequest = new RestRequest($"/rates/{populatedURL}", Method.GET);

            var response = await client.GetAsync<TaxJarRatesResponse_Model>(restRequest);

            return response.Rate.CombinedRate;
        }

        /// <summary>
        /// Method to add the additional optional parameters to the GET request URL
        /// </summary>
        /// <param name="taxJarRatesRequest_Model"></param>
        private string LocationTaxRatesURLBuilder(GetLocationTaxRateRequest request)
        {
            StringBuilder urlBuilder = new StringBuilder();
            List<string> parametersToAdd = new List<string>();

            if (!string.IsNullOrEmpty(request.Country))
            {
                parametersToAdd.Add($"country={request.Country}");
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                parametersToAdd.Add($"city={request.City}");
            }
            if (!string.IsNullOrEmpty(request.State))
            {
                parametersToAdd.Add($"state={request.State}");
            }
            if (!string.IsNullOrEmpty(request.StreetAddress))
            {
                parametersToAdd.Add($"street={request.StreetAddress}");
            }

            urlBuilder.Append((parametersToAdd.Count > 0) ? $"{request.ZipCode}?" : request.ZipCode);

            for (int index = 0; index < parametersToAdd.Count; index++)
            {
                urlBuilder.Append(index > 0 ? "&" : string.Empty);

                urlBuilder.Append(parametersToAdd[index]);
            }

            return urlBuilder.ToString();
        }
    }
}

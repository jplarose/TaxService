using System;
using System.Threading.Tasks;
using TaxService.Models;
using TaxService.Models.Models.Domain;

namespace TaxService
{
    public class TaxService
    {
        readonly ITaxServiceProviderBase taxServiceProvider;

        public TaxService(TaxCalculators taxCalculators)
        {
            taxServiceProvider = getTaxServiceProvider(taxCalculators);
        }

        private ITaxServiceProviderBase getTaxServiceProvider(TaxCalculators calculator)
        {
            ITaxServiceProviderBase provider = null;
            switch (calculator)
            {
                case TaxCalculators.TaxJar:
                    provider = new TaxServiceProvider.TaxJar.TaxServiceProvider();
                    break;
                default:
                    break;
            }

            return provider;
        }

        /// <summary>
        /// Method to calculate the total tax on a sale
        /// </summary>
        /// <param name="calculateTaxRequest"></param>
        /// <returns>A decimal representation of tax calculated based on the information provided</returns>
        public async Task<decimal> CalculateTax(TaxServiceRequest calculateTaxRequest)
        {
            // At minimum you need to have a sale amount, country, state, and zip code to determine the tax
            if (calculateTaxRequest == null 
                || calculateTaxRequest.SaleAmount.Equals(0) 
                || String.IsNullOrEmpty(calculateTaxRequest.Customer?.ZipCode) 
                || String.IsNullOrEmpty(calculateTaxRequest.Customer?.Country)
                || String.IsNullOrEmpty(calculateTaxRequest.Customer?.State))
            {
                throw new InvalidOperationException("Invalid Object Provided");
            }
            return await taxServiceProvider.CalculateTax(calculateTaxRequest);
        }

        /// <summary>
        /// Method to get the tax for a specific location provided by the user
        /// </summary>
        /// <param name="LocationTaxRatesRequest"></param>
        /// <returns>A decimal representation of the tax for the specified location</returns>
        public async Task<decimal> GetLocationTaxRates(TaxServiceRequest LocationTaxRatesRequest)
        {
            if (String.IsNullOrEmpty(LocationTaxRatesRequest.Customer.ZipCode))
            {
                throw new InvalidOperationException("ZIP Code Required");
            }
            return await taxServiceProvider.GetLocationTaxes(LocationTaxRatesRequest);
        }

    }
}

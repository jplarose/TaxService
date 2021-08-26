using System;
using System.Threading.Tasks;
using TaxService.Models.Models.Domain;

namespace TaxService
{
    public class TaxService
    {
        readonly ITaxServiceProvider _taxServiceProvider;

        public TaxService(ITaxServiceProvider taxServiceProvider)
        {
            _taxServiceProvider = taxServiceProvider;
        }

        /// <summary>
        /// Method to calculate the total tax on a sale
        /// </summary>
        /// <param name="calculateTaxRequest"></param>
        /// <returns>A decimal representation of tax calculated based on the information provided</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<decimal> CalculateTax(CalculateTaxRequest calculateTaxRequest)
        {
            if (calculateTaxRequest == null)
            {
                throw new ArgumentException($"{calculateTaxRequest} cannot be null");
            }

            // At minimum you need to have a sale amount, country, state, and zip code to determine the tax
            calculateTaxRequest.Validate();

            return await _taxServiceProvider.CalculateTax(calculateTaxRequest);
        }

        /// <summary>
        /// Method to get the tax for a specific location provided by the user
        /// </summary>
        /// <param name="LocationTaxRatesRequest"></param>
        /// <returns>A decimal representation of the tax for the specified location</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<decimal> GetLocationTaxRates(GetLocationTaxRateRequest locationTaxRatesRequest)
        {
            if (String.IsNullOrEmpty(locationTaxRatesRequest.ZipCode))
            {
                throw new ArgumentException("ZIP Code Required");
            }
            return await _taxServiceProvider.GetLocationTaxes(locationTaxRatesRequest);
        }

    }
}

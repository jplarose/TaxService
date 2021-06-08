using System;
using System.Threading.Tasks;
using TaxService.Models;

namespace TaxService
{
    public class TaxService
    {
        TaxCalculators _taxCalculators;
        public TaxService(TaxCalculators taxCalculators)
        {
            _taxCalculators = taxCalculators;
        }

        public async Task<string> CalculateTax()
        {

            return "";
        }

        public async Task<string> GetLocationTaxRates()
        {
            return "";
        }

    }
}

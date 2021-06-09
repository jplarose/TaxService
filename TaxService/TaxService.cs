using System;
using System.Threading.Tasks;
using TaxService.Models;
using TaxService.Models.TaxJar;

namespace TaxService
{
    public class TaxService
    {
        TaxCalculators _taxCalculators;
        public TaxService(TaxCalculators taxCalculators)
        {
            _taxCalculators = taxCalculators;
        }

        public async Task<string> CalculateTax(TaxJarCalculateTax_Model calculateTax_Model = null)
        {
            switch (_taxCalculators)
            {
                case TaxCalculators.TaxJar:
                    if (calculateTax_Model != null)
                    {
                        // Call over to TaxJar specific logic with it
                    }
                    else
                    {
                        // return error
                    }
                    break;
                default:
                    // Not a valid Tax Calculator provided
                    // Create an error response and return it
                    break;
            }

            return "";
        }

        public async Task<string> GetLocationTaxRates()
        {
            switch (_taxCalculators)
            {
                case TaxCalculators.TaxJar:
                    break;
                default:
                    // Not a valid Tax Calculator provided
                    // Create an error response and return it
                    break;
            }

            return "";
        }

    }
}

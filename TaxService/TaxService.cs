using System;
using System.Threading.Tasks;
using TaxService.Logic;
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

        public async Task<decimal> CalculateTax(TaxJarCalculateTax_Model calculateTax_Model = null)
        {

            switch (_taxCalculators)
            {
                case TaxCalculators.TaxJar:
                    // Initialize the TaxJar logic class
                    TaxJarLogic taxJarLogic = new TaxJarLogic();

                    if (calculateTax_Model != null)
                    {
                        // Call over to TaxJar specific logic with it
                        return await taxJarLogic.CalculateTax(calculateTax_Model);
                    }
                    else
                    {
                        // Whatever is consuming this can handle the error thrown
                        throw new Exception("Invalid TaxJar Calculate Tax model provided for Request.");
                    }
                default:
                    // Not a valid Tax Calculator provided
                    // Create an error response and return it
                    break;
            }

            return 1;
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

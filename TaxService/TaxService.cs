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
                        return await taxJarLogic.CalculateTax(calculateTax_Model);
                    }
                    else
                    {
                        // Whatever is consuming this can handle the error thrown
                        throw new Exception("Invalid TaxJar Calculate Tax model provided for Request.");
                    }
                default:
                    throw new Exception("Invalid Request");
            }
        }

        public async Task<decimal> GetLocationTaxRates(TaxJarRatesRequest_Model taxJarRatesRequest_Model)
        {
            switch (_taxCalculators)
            {
                case TaxCalculators.TaxJar:
                    // Initialize the TaxJar logic class
                    TaxJarLogic taxJarLogic = new TaxJarLogic();

                    if (!string.IsNullOrEmpty(taxJarRatesRequest_Model.ZIP))
                    {
                        return await taxJarLogic.GetLocationTaxRates(taxJarRatesRequest_Model);
                    }
                    else
                    {
                        // Whatever is consuming this can handle the error thrown
                        throw new Exception("ZIP Code required for request");
                    }
                default:
                    throw new Exception("Invalid Request");
            }
        }

    }
}

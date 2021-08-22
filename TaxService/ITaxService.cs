using System.Threading.Tasks;
using TaxService.Models.Models.Domain;

namespace TaxService
{
    public interface ITaxService
    {
        Task<decimal> CalculateTax(CalculateTaxRequest calculateTaxRequest);
        Task<decimal> GetLocationTaxRates(GetLocationTaxRateRequest LocationTaxRatesRequest);
    }
}
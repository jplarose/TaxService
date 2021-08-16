using System.Threading.Tasks;
using TaxService.Models.Models.Domain;

namespace TaxService
{
    public interface ITaxService
    {
        Task<decimal> CalculateTax(TaxServiceRequest calculateTaxRequest);
        Task<decimal> GetLocationTaxRates(TaxServiceRequest LocationTaxRatesRequest);
    }
}
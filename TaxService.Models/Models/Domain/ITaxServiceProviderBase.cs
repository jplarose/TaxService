using System.Threading.Tasks;

namespace TaxService.Models.Models.Domain
{
    public interface ITaxServiceProvider
    {
        Task<decimal> CalculateTax(CalculateTaxRequest request);
        Task<decimal> GetLocationTaxes(GetLocationTaxRateRequest request);
    }
}
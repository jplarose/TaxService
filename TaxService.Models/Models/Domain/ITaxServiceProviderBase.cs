using System.Threading.Tasks;

namespace TaxService.Models.Models.Domain
{
    public interface ITaxServiceProvider
    {
        Task<decimal> CalculateTax(TaxServiceRequest request);
        Task<decimal> GetLocationTaxes(TaxServiceRequest request);
    }
}
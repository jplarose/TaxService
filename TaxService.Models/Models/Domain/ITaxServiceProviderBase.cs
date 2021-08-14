using System.Threading.Tasks;

namespace TaxService.Models.Models.Domain
{
    public interface ITaxServiceProviderBase
    {
        Task<decimal> CalculateTax(TaxServiceRequest request);
        Task<decimal> GetLocationTaxes(TaxServiceRequest request);
    }
}
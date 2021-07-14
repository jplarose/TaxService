using System.Threading.Tasks;
using TaxService.Models.TaxJar;

namespace TaxService.DataAccess.TaxJar
{
    public interface ITaxJarDAL
    {
        Task<TaxJarCalculateTaxResponse_Model> CalculateTax(TaxJarCalculateTax_Model taxJarCalculateTax_Model);
        Task<TaxJarRatesResponse_Model> GetLocationTaxRates(string ratesRequestURLString);
    }
}
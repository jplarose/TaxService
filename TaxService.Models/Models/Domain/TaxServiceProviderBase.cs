using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxService.Models.Models.Domain
{
    public abstract class TaxServiceProviderBase : ITaxServiceProviderBase
    {
        public virtual TaxCalculators TaxCalculators { get; }
        public abstract Task<decimal> CalculateTax(TaxServiceRequest request);
        public abstract Task<decimal> GetLocationTaxes(TaxServiceRequest request);


    }
}

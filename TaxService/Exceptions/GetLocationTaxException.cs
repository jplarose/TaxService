using System;
using System.Collections.Generic;
using System.Text;

namespace TaxService.Exceptions
{
    public class GetLocationTaxException: Exception
    {
        public GetLocationTaxException(String message) : base(message) { }
    }
}

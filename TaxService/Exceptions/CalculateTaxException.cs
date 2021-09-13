using System;
using System.Collections.Generic;
using System.Text;

namespace TaxService.Exceptions
{
    public class CalculateTaxException: Exception
    {
        public CalculateTaxException(String message) : base(message) { }
    }
}

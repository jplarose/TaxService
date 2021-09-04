using System;
using System.Collections.Generic;
using System.Text;

namespace TaxService.Exceptions
{
    public class TaxServiceException: Exception
    {
        public TaxServiceException(String message) : base(message) { }
    }
}

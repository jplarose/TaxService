using System;
using System.Collections.Generic;
using System.Text;

namespace TaxService.Models.Models.Domain
{
    public class TaxServiceRequest
    {
        public Customer Customer { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal Shipping { get; set; }
    }

    public class Customer
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

    }
}

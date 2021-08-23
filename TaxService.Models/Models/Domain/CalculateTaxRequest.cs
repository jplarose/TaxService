

namespace TaxService.Models.Models.Domain
{
    public class CalculateTaxRequest
    {
        public Customer Customer { get; set; } = new Customer();
        public decimal SaleAmount { get; set; }
        public decimal Shipping { get; set; }
        public string FromZipCode { get; set; }
        public string FromState { get; set; }
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

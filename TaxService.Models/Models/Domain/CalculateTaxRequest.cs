using System;

namespace TaxService.Models.Models.Domain
{
    public class CalculateTaxRequest
    {

        public CalculateTaxRequest()
        {

        }

        public CalculateTaxRequest(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException($"{customer} cannot be null");
            }
            Customer = customer;
        }
        public Customer Customer { get; } = new Customer();
        public decimal SaleAmount { get; set; }
        public decimal Shipping { get; set; }
        public string FromZipCode { get; set; } = string.Empty;
        public string FromState { get; set; } = string.Empty;

        public void Validate()
        {
            if (SaleAmount.Equals(0)
                || String.IsNullOrEmpty(Customer.ZipCode)
                || String.IsNullOrEmpty(Customer.Country)
                || String.IsNullOrEmpty(Customer.State))
            {
                throw new ArgumentException("Invalid Object Provided");
            }
        }
    }
}

    public class Customer
    {
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

    }



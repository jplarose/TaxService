//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar.Models;

namespace TaxServiceUnitTests
{
    //[TestClass]
    public class CalculateTax_Tests
    {
        private class Setup
        {
            public readonly TaxService.TaxService taxServiceTaxJar;

            public Setup()
            {
                taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar);
            }

            public TaxServiceRequest getvalidTaxServiceRequest()
            {
                return new TaxServiceRequest()
                {
                    SaleAmount = 15,
                    Shipping = 1.5m,
                    Customer = new Customer
                    {
                        ZipCode = "90002",
                        Country = "US",
                        State = "CA"
                    }
                };
            }
        }

        [Fact(DisplayName = "ValidTaxCalculatorRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidTaxCalculatorRequest()
        {
            // Arrange
            var setup = new Setup();
            var mockCalculateTaxRequest = setup.getvalidTaxServiceRequest();

            // Act
            var response = await setup.taxServiceTaxJar.CalculateTax(mockCalculateTaxRequest);

            // Assert
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "InvalidTaxRequestObject")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject()
        {
            var setup = new Setup();
            TaxServiceRequest request = null;

            // No request object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName ="InvalidTaxRequestObject_NoAmountGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoAmountGiven()
        {
            var setup = new Setup();
            TaxServiceRequest request = new TaxServiceRequest()
            {
                Customer = new Customer
                {
                    ZipCode = "04062"
                }
            };

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoZIPGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoZIPGiven()
        {
            var setup = new Setup();
            TaxServiceRequest request = new TaxServiceRequest()
            {
                SaleAmount = 15
            };

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoCountryGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoCountryGiven()
        {
            var setup = new Setup();
            TaxServiceRequest request = new TaxServiceRequest()
            {
                SaleAmount = 15,
                Shipping = 1,
                Customer = new Customer
                {
                    ZipCode = "04062",
                    State = "ME"
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.CalculateTax(request));
        }




    }
}

using System;
using System.Threading.Tasks;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar;
using Xunit;

namespace TaxService.Tests.Integration.TaxJar
{
    public class CalculateTax_IntegrationTests
    {
        private class Setup
        {
            public readonly TaxService _taxServiceTaxJar;

            public Setup()
            {
                _taxServiceTaxJar = new TaxService(new TaxJarServiceProvider());
            }

            public CalculateTaxRequest getvalidTaxServiceRequest()
            {
                return new CalculateTaxRequest()
                {
                    SaleAmount = 15,
                    Shipping = 1.5m,
                    Customer = new Customer
                    {
                        ZipCode = "04062",
                        Country = "US",
                        State = "ME"
                    }
                };
            }
        }

        [Fact(DisplayName = "ValidTaxCalculatorRequest")]
        [Trait("Category", "IntegrationTest")]
        public async Task ValidTaxCalculatorRequest()
        {
            // Arrange
            var setup = new Setup();
            var mockCalculateTaxRequest = setup.getvalidTaxServiceRequest();

            // Act
            var response = await setup._taxServiceTaxJar.CalculateTax(mockCalculateTaxRequest);

            // Assert
            Assert.NotEqual(0, response);
        }

        [Fact(DisplayName = "InvalidTaxRequestObject")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxRequestObject()
        {
            var setup = new Setup();
            CalculateTaxRequest request = null;

            // No request object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup._taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoAmountGiven")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxRequestObject_NoAmountGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest()
            {
                Customer = new Customer
                {
                    ZipCode = "04062"
                }
            };

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup._taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoZIPGiven")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxRequestObject_NoZIPGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest()
            {
                SaleAmount = 15
            };

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup._taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoCountryGiven")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxRequestObject_NoCountryGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest()
            {
                SaleAmount = 15,
                Shipping = 1,
                Customer = new Customer
                {
                    ZipCode = "04062",
                    State = "ME"
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => setup._taxServiceTaxJar.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoStateGiven")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxRequestObject_NoStateGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest()
            {
                SaleAmount = 15,
                Shipping = 1,
                Customer = new Customer
                {
                    ZipCode = "04062",
                    Country = "US"
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => setup._taxServiceTaxJar.CalculateTax(request));
        }




    }
}

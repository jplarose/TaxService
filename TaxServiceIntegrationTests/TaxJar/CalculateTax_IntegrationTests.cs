using System;
using System.Threading.Tasks;
using TaxService.Exceptions;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar;
using Xunit;

namespace TaxService.Tests.Integration.TaxJar
{
    public class CalculateTax_IntegrationTests
    {
        private class Setup
        {
            public readonly TaxJarServiceProvider _taxJarService;

            public Setup()
            {
                _taxJarService = new TaxJarServiceProvider();
            }

            public CalculateTaxRequest GetvalidTaxServiceRequest()
            {
                return new CalculateTaxRequest(new Customer
                {
                    ZipCode = "04062",
                    Country = "US",
                    State = "ME"
                })
                {
                    SaleAmount = 15,
                    Shipping = 1.5m,
                    FromZipCode = "04062",
                    FromState = "ME"
                };
            }
        }

        [Fact(DisplayName = "ValidTaxCalculatorRequest")]
        [Trait("Category", "IntegrationTest")]
        public async Task ValidTaxCalculatorRequest()
        {
            // Arrange
            var setup = new Setup();
            var mockCalculateTaxRequest = setup.GetvalidTaxServiceRequest();

            // Act
            var response = await setup._taxJarService.CalculateTax(mockCalculateTaxRequest);

            // Assert
            Assert.NotEqual(0, response);
        }

        [Fact(DisplayName = "InvalidTaxCalculatorRequest")]
        [Trait("Category", "IntegrationTest")]
        public async Task InvalidTaxCalculatorRequest()
        {
            // Arrange
            var setup = new Setup();
            var mockCalculateTaxRequest = new CalculateTaxRequest();

            // Assert
            await Assert.ThrowsAsync<TaxServiceException>(() => setup._taxJarService.CalculateTax(mockCalculateTaxRequest));
        }
    }
}

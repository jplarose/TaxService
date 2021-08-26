﻿using System.Threading.Tasks;
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

            public CalculateTaxRequest getvalidTaxServiceRequest()
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
            var mockCalculateTaxRequest = setup.getvalidTaxServiceRequest();

            // Act
            var response = await setup._taxJarService.CalculateTax(mockCalculateTaxRequest);

            // Assert
            Assert.NotEqual(0, response);
        }
    }
}

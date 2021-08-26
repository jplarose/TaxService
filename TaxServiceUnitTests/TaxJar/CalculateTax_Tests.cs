using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TaxService.Models.Models.Domain;

namespace TaxService.Tests.Unit
{
    public class CalculateTax_Tests
    {
        private class Setup
        {
            public readonly TaxService _taxService;
            Mock<ITaxServiceProvider> _mockTaxService = new Mock<ITaxServiceProvider>();

            public Setup()
            {
                _mockTaxService.Setup(m => m.CalculateTax(It.IsAny<CalculateTaxRequest>())).ReturnsAsync(1m);
                _taxService = new TaxService(_mockTaxService.Object);
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
        [Trait("Category", "UnitTest")]
        public async Task ValidTaxCalculatorRequest()
        {

            // Arrange
            var setup = new Setup();
            var mockCalculateTaxRequest = setup.getvalidTaxServiceRequest();

            // Act
            var response = await setup._taxService.CalculateTax(mockCalculateTaxRequest);

            // Assert
            Assert.NotEqual(0, response);
        }

        [Fact(DisplayName = "InvalidTaxRequestObject")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject()
        {
            var setup = new Setup();
            CalculateTaxRequest request = null;

            // No request object passed in, returns exception
            await Assert.ThrowsAsync<ArgumentException>(() => setup._taxService.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoAmountGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoAmountGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest(new Customer
            {
                ZipCode = "04062"
            });

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<ArgumentException>(() => setup._taxService.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoZIPGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoZIPGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest()
            {
                SaleAmount = 15
            };

            // Invalid object passed in, returns exception
            await Assert.ThrowsAsync<ArgumentException>(() => setup._taxService.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoCountryGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoCountryGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest(new Customer
            {
                ZipCode = "04062",
                State = "ME"
            })
            {
                SaleAmount = 15,
                Shipping = 1
            };

            await Assert.ThrowsAsync<ArgumentException>(() => setup._taxService.CalculateTax(request));
        }

        [Fact(DisplayName = "InvalidTaxRequestObject_NoStateGiven")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject_NoStateGiven()
        {
            var setup = new Setup();
            CalculateTaxRequest request = new CalculateTaxRequest(new Customer
            {
                ZipCode = "04062",
                Country = "US"
            })
            {
                SaleAmount = 15,
                Shipping = 1
            };

            await Assert.ThrowsAsync<ArgumentException>(() => setup._taxService.CalculateTax(request));
        }
    }
}

using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar;

namespace TaxService.Tests.Unit
{
    public class CalculateTax_Tests
    {
        [Fact(DisplayName = "ValidTaxCalculatorRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidTaxCalculatorRequest()
        {
            // Arrange
            var _taxServiceTaxJar = new Mock<ITaxService>();
            _taxServiceTaxJar.Setup(m => m.CalculateTax(It.IsAny<CalculateTaxRequest>())).ReturnsAsync(0.055m);

            // Act
            var response = await _taxServiceTaxJar.Object.CalculateTax(new CalculateTaxRequest());

            // Assert
            Assert.NotEqual(0, response);
        }

        [Fact(DisplayName = "InvalidTaxRequestObject")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxRequestObject()
        {
            CalculateTaxRequest request = null;
            var _taxServiceTaxJar = new Mock<ITaxService>();
            _taxServiceTaxJar.Setup(m => m.CalculateTax(request)).ThrowsAsync(new InvalidOperationException());

            // No request object passed in, returns exception
            await Assert.ThrowsAsync<InvalidOperationException>(() => _taxServiceTaxJar.Object.CalculateTax(request));
        }
    }
}

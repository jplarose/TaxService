using System;
using System.Threading.Tasks;
using Xunit;
using TaxService.Models.Models.Domain;
using Moq;

namespace TaxService.Tests.Unit
{
    public class GetLocationTaxRates_Tests
    {
        [Fact(DisplayName = "ValidLocationTaxRatesRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidLocationTaxRatesRequest()
        {
            var mockTaxService = new Mock<ITaxService>();
            mockTaxService.Setup(m => m.GetLocationTaxRates(It.IsAny<GetLocationTaxRateRequest>())).ReturnsAsync(0.055m);

            decimal response = await mockTaxService.Object.GetLocationTaxRates(new GetLocationTaxRateRequest());

            Assert.Equal(0.055m, response);
        }

        [Fact(DisplayName = "InvalidLocationTaxRatesRequest")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidLocationTaxRatesRequest()
        {
            var mockTaxService = new Mock<ITaxService>();
            mockTaxService.Setup(m => m.GetLocationTaxRates(It.IsAny<GetLocationTaxRateRequest>())).ThrowsAsync(new InvalidOperationException());

            await Assert.ThrowsAsync<InvalidOperationException>(() => mockTaxService.Object.GetLocationTaxRates(new GetLocationTaxRateRequest()));
        }
    }
}

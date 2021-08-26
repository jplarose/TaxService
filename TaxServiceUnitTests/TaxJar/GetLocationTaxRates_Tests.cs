using System;
using System.Threading.Tasks;
using Xunit;
using TaxService.Models.Models.Domain;
using Moq;

namespace TaxService.Tests.Unit
{
    public class GetLocationTaxRates_Tests
    {
        public decimal ValidResponse => 0.055m;

        private class Setup
        {
            public readonly TaxService taxServiceTaxJar;
            Mock<ITaxServiceProvider> _mockService = new Mock<ITaxServiceProvider>();
            public Setup(decimal desiredResponse)
            {
                _mockService.Setup(m => m.GetLocationTaxes(It.IsAny<GetLocationTaxRateRequest>())).ReturnsAsync(desiredResponse);
                taxServiceTaxJar = new TaxService(_mockService.Object);
            }

            public GetLocationTaxRateRequest getMockTaxJarRatesRequest(string zip = null, string state = null, string city = null, string country = null, string street = null)
            {
                return new GetLocationTaxRateRequest()
                {
                    ZipCode = zip,
                    State = state,
                    City = city,
                    Country = country,
                    StreetAddress = street
                };
            }
        }


        [Fact(DisplayName = "ValidLocationTaxRatesRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidLocationTaxRatesRequest()
        {
            Setup setup = new Setup(ValidResponse);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062");

            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIP")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_NoZIP()
        {

            Setup setup = new Setup(0);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest();

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<ArgumentException>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIPButOtherInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_NoZIPButOtherInfo()
        {
            Setup setup = new Setup(0);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest(null, "ME", null, "US");

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<ArgumentException>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_FullInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_FullInfo()
        {
            Setup setup = new Setup(ValidResponse);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062", "ME", "Windham", "US", "73 Whites Bridge Road");


            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);

        }
    }
}

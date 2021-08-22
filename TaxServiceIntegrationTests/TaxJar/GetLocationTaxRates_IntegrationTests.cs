using System;
using System.Threading.Tasks;
using TaxService.Models.Models.Domain;
using TaxServiceProvider.TaxJar;
using Xunit;

namespace TaxService.Tests.Integration.TaxJar
{
    public class GetLocationTaxRates_IntegrationTests
    {
        private class Setup
        {
            public readonly TaxService taxServiceTaxJar;
            public Setup()
            {
                taxServiceTaxJar = new TaxService(new TaxJarServiceProvider());
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
        [Trait("Category", "IntegrationTest")]
        public async Task ValidLocationTaxRatesRequest()
        {
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062");

            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIP")]
        [Trait("Category", "IntegrationTest")]
        public async Task LocationTaxRatesRequest_NoZIP()
        {

            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest();

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIPButOtherInfo")]
        [Trait("Category", "IntegrationTest")]
        public async Task LocationTaxRatesRequest_NoZIPButOtherInfo()
        {
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest(null, "ME", null, "US");

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        [Fact(DisplayName = "LocationTaxRatesRequest_FullInfo")]
        [Trait("Category", "IntegrationTest")]
        public async Task LocationTaxRatesRequest_FullInfo()
        {
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062", "ME", "Windham", "US", "73 Whites Bridge Road");


            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);

        }
    }
}

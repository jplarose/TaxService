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

            public readonly TaxJarServiceProvider _taxService;
            public Setup()
            {
                taxServiceTaxJar = new TaxService(new TaxJarServiceProvider());
                _taxService = new TaxJarServiceProvider();
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

            decimal response = await setup._taxService.GetLocationTaxes(mockRatesRequest);

            Assert.Equal(0.055m, response);
        }
    }
}

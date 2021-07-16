//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Models.TaxJar;
using Moq;
using Xunit;
using TaxService.DataAccess.TaxJar;

namespace TaxServiceUnitTests
{
    //[TestClass]
    public class GetLocationTaxRates_Tests
    {
        private class Setup
        {
            public Mock<ITaxJarDAL> mockTaxJarDAL = new Mock<ITaxJarDAL>();
            public readonly TaxService.TaxService taxServiceTaxJar;
            public Setup()
            {
                taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar, mockTaxJarDAL.Object);
            }

            public TaxJarRatesResponse_Model getMockTaxJarRatesResponse(string zip, decimal expectedRateResponse)
            {
                TaxJarRatesResponse_Model response = new TaxJarRatesResponse_Model()
                {
                    Rate = new RatesResponseAttributes()
                    {
                        CombinedRate = expectedRateResponse,
                        Zip = zip
                    }
                };

                return response;
            }

            public TaxJarRatesRequest_Model getMockTaxJarRatesRequest (string zip = null, string state = null, string city = null, string country = null, string street = null)
            {
                return new TaxJarRatesRequest_Model()
                {
                    ZIP = zip,
                    State = state,
                    City = city,
                    Country = country,
                    Street = street
                };
            }
        }

        //[TestMethod]
        [Fact(DisplayName = "ValidLocationTaxRatesRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidLocationTaxRatesRequest()
        {
            Setup setup = new Setup();
            var taxJarRatesResponse = setup.getMockTaxJarRatesResponse("04062", 0.055m);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062");

            setup.mockTaxJarDAL.Setup(x => x.GetLocationTaxRates(It.IsAny<string>())).Returns(Task.FromResult(taxJarRatesResponse));
            

            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIP")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_NoZIP()
        {

            Setup setup = new Setup();
            var taxJarRatesResponse = setup.getMockTaxJarRatesResponse("04062", 0.055m);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest();

            setup.mockTaxJarDAL.Setup(x => x.GetLocationTaxRates(It.IsAny<string>())).Returns(Task.FromResult(taxJarRatesResponse));
            
            // No ZIP defined, it is a required field. Should throw Exception
            await Assert.ThrowsAsync<Exception>(() =>  setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIPButOtherInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_NoZIPButOtherInfo()
        {
            Setup setup = new Setup();
            var taxJarRatesResponse = setup.getMockTaxJarRatesResponse("04062", 0.055m);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest(null, "ME", null, "US");

            setup.mockTaxJarDAL.Setup(x => x.GetLocationTaxRates(It.IsAny<string>())).Returns(Task.FromResult(taxJarRatesResponse));

            // No ZIP defined, it is a required field. Should throw Exception
            await Assert.ThrowsAsync<Exception>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        //[TestMethod]
        [Fact(DisplayName = "LocationTaxRatesRequest_FullInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_FullInfo()
        {           
            Setup setup = new Setup();
            var taxJarRatesResponse = setup.getMockTaxJarRatesResponse("04062", 0.055m);
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062", "ME", "Windham", "US", "73 Whites Bridge Road");

            setup.mockTaxJarDAL.Setup(x => x.GetLocationTaxRates(It.IsAny<string>())).Returns(Task.FromResult(taxJarRatesResponse));

            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);

        }
    }
}

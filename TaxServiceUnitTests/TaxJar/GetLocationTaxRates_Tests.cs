//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TaxService.Models.Models.Domain;

namespace TaxServiceUnitTests
{
    //[TestClass]
    public class GetLocationTaxRates_Tests
    {
        private class Setup
        {
            public readonly TaxService.TaxService taxServiceTaxJar;
            public Setup()
            {
                taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar);
            }

            public TaxServiceRequest getMockTaxJarRatesRequest (string zip = null, string state = null, string city = null, string country = null, string street = null)
            {
                return new TaxServiceRequest()
                {
                    Customer = new Customer()
                    {
                        ZipCode = zip,
                        State = state,
                        City = city,
                        Country = country,
                        StreetAddress = street
                    }
                    
                };
            }
        }

        //[TestMethod]
        [Fact(DisplayName = "ValidLocationTaxRatesRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidLocationTaxRatesRequest()
        {
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062");            

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
            var mockRatesRequest = setup.getMockTaxJarRatesRequest();

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<InvalidOperationException>(() =>  setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        [Fact(DisplayName = "LocationTaxRatesRequest_NoZIPButOtherInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_NoZIPButOtherInfo()
        {
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest(null, "ME", null, "US");

            // No ZIP defined, it is a required field. Should throw NullReferenceException
            await Assert.ThrowsAsync<InvalidOperationException>(() => setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest));
        }

        //[TestMethod]
        [Fact(DisplayName = "LocationTaxRatesRequest_FullInfo")]
        [Trait("Category", "UnitTest")]
        public async Task LocationTaxRatesRequest_FullInfo()
        {           
            Setup setup = new Setup();
            var mockRatesRequest = setup.getMockTaxJarRatesRequest("04062", "ME", "Windham", "US", "73 Whites Bridge Road");


            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(mockRatesRequest);

            Assert.Equal(0.055m, response);

        }
    }
}

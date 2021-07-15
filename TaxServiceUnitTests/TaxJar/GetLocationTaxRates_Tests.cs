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

            public TaxJarRatesResponse_Model getMockTaxJarRatesResponse(string zip)
            {
                TaxJarRatesResponse_Model response = new TaxJarRatesResponse_Model()
                {
                    Rate = new RatesResponseAttributes()
                    {
                        StandardRate = 0.055m,
                        Zip = zip
                    }
                };

                return response;
            }
        }

        //[TestMethod]
        public async Task ValidLocationTaxRatesRequest()
        {
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model
            {
                ZIP = "04062"
            };

            Setup setup = new Setup();
            var taxJarRatesResponse = setup.getMockTaxJarRatesResponse("04062");
            setup.mockTaxJarDAL.Setup(x => x.GetLocationTaxRates(It.IsAny<string>())).Returns(Task.FromResult(taxJarRatesResponse));
            

            decimal response = await setup.taxServiceTaxJar.GetLocationTaxRates(request);

            Assert.Equal(0.055m, response);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        public async Task LocationTaxRatesRequest_NoZIP()
        {
            // No ZIP defined, it is a required field. Should throw Exception
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model();

            decimal response = await taxServiceTaxJar.GetLocationTaxRates(request);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        public async Task LocationTaxRatesRequest_NoZIPButOtherInfo()
        {
            // No ZIP defined, it is a required field. Should throw Exception
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model
            {
                Country = "US",
                State = "ME"
            };

            decimal response = await taxServiceTaxJar.GetLocationTaxRates(request);
        }

        //[TestMethod]
        public async Task LocationTaxRatesRequest_FullInfo()
        {
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model
            {
                ZIP = "04062",
                City = "Windham",
                Country = "US",
                State = "ME",
                Street = "73 Whites Bridge Road"
            };

            decimal response = await taxServiceTaxJar.GetLocationTaxRates(request);

            // Value determined via independent test from a Postman request
            Assert.AreEqual(0.055m, response);

        }
    }
}

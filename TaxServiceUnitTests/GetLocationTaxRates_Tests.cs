using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Models.TaxJar;

namespace TaxServiceUnitTests
{
    [TestClass]
    public class GetLocationTaxRates_Tests
    {
        TaxService.TaxService taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar);

        [TestMethod]
        public async Task ValidLocationTaxRatesRequest()
        {
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model
            {
                ZIP = "04062"
            };

            decimal response = await taxServiceTaxJar.GetLocationTaxRates(request);

            Assert.AreEqual(0.055m, response);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task LocationTaxRatesRequest_NoZIP()
        {
            // No ZIP defined, it is a required field. Should throw Exception
            TaxJarRatesRequest_Model request = new TaxJarRatesRequest_Model();

            decimal response = await taxServiceTaxJar.GetLocationTaxRates(request);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
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

        [TestMethod]
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

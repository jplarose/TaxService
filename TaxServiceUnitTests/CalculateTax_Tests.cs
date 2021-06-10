using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService;
using TaxService.Models.TaxJar;
using TaxService.Logic;
using System.Threading.Tasks;

namespace TaxServiceUnitTests
{
    [TestClass]
    public class CalculateTax_Tests
    {
        TaxService.TaxService taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar);

        [TestMethod]
        public async Task ValidTaxCalculatorRequest()
        {

            TaxJarCalculateTax_Model request = new TaxJarCalculateTax_Model
            {
                Amount = 15.00m,
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                Shipping = 1.5m,
                FromCity = "La Jolla",
                FromCountry = "US",
                FromState = "CA",
                FromStreet = "9500 Gilman Drive",
                FromZip = "92093"
            };

            decimal response = await taxServiceTaxJar.CalculateTax(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(response, 1.54m);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task InvalidTaxCalculatorRequest()
        {
            // No request object passed in, returns exception
            decimal response = await taxServiceTaxJar.CalculateTax();
        }

    }
}

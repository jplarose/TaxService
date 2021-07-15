//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaxService;
using TaxService.Models.TaxJar;
using TaxService.Logic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TaxService.DataAccess.TaxJar;

namespace TaxServiceUnitTests
{
    //[TestClass]
    public class CalculateTax_Tests
    {
        private class Setup
        {
            public Mock<ITaxJarDAL> mockTaxJarDAL = new Mock<ITaxJarDAL>();
            public readonly TaxService.TaxService taxServiceTaxJar;

            public Setup()
            {
                taxServiceTaxJar = new TaxService.TaxService(TaxService.Models.TaxCalculators.TaxJar, mockTaxJarDAL.Object);
            }

        }

        public async Task ValidTaxCalculatorRequest()
        {
            var setup = new Setup();
            setup.mockTaxJarDAL.Setup(x => x.CalculateTax(It.IsAny<TaxJarCalculateTax_Model>())).Returns(Task.FromResult(It.IsAny<TaxJarCalculateTaxResponse_Model>()));
            

            TaxJarCalculateTax_Model request = new TaxJarCalculateTax_Model
            {
                Amount = 15,
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

            decimal response = await setup.taxServiceTaxJar.CalculateTax(request);

            Assert.NotNull(response);

            // Value determined via independent test from a Postman request 
            Assert.Equal(1.54m, response);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        public async Task InvalidTaxCalculatorRequest()
        {
            var setup = new Setup();
            setup.mockTaxJarDAL.Setup(x => x.CalculateTax(It.IsAny<TaxJarCalculateTax_Model>())).Returns(Task.FromResult(It.IsAny<TaxJarCalculateTaxResponse_Model>()));
            // No request object passed in, returns exception
            decimal response = await setup.taxServiceTaxJar.CalculateTax();

            await Assert.ThrowsAsync<Exception>(() => setup.taxServiceTaxJar.CalculateTax());
        }

    }
}

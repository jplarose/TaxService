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

            public TaxJarCalculateTax_Model getValidMockCalculateTaxModel()
            {
                return new TaxJarCalculateTax_Model()
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
            }

            public TaxJarCalculateTaxResponse_Model getMockCalculateTaxResponse()
            {
                return new TaxJarCalculateTaxResponse_Model()
                {
                    Tax = new Tax()
                    {
                        AmountToCollect = 1.54m
                    }
                };
            }

        }

        [Fact(DisplayName = "ValidTaxCalculatorRequest")]
        [Trait("Category", "UnitTest")]
        public async Task ValidTaxCalculatorRequest()
        {
            var setup = new Setup();
            var mockCalculateTaxRequest = setup.getValidMockCalculateTaxModel();
            var mockCalculateTaxResponse = setup.getMockCalculateTaxResponse();

            setup.mockTaxJarDAL.Setup(x => x.CalculateTax(It.IsAny<TaxJarCalculateTax_Model>())).Returns(Task.FromResult(mockCalculateTaxResponse));

            decimal response = await setup.taxServiceTaxJar.CalculateTax(mockCalculateTaxRequest);

            // Value determined via independent test from a Postman request 
            Assert.Equal(1.54m, response);
        }

        [Fact(DisplayName = "InvalidTaxCalculatorRequest")]
        [Trait("Category", "UnitTest")]
        public async Task InvalidTaxCalculatorRequest()
        {
            var setup = new Setup();
            var mockCalculateTaxResponse = setup.getMockCalculateTaxResponse();

            setup.mockTaxJarDAL.Setup(x => x.CalculateTax(It.IsAny<TaxJarCalculateTax_Model>())).Returns(Task.FromResult(mockCalculateTaxResponse));

            // No request object passed in, returns exception
            Exception ex = await Assert.ThrowsAsync<Exception>(() => setup.taxServiceTaxJar.CalculateTax());

            Assert.Equal("Invalid TaxJar Calculate Tax model provided for Request.", ex.Message);
        }

    }
}

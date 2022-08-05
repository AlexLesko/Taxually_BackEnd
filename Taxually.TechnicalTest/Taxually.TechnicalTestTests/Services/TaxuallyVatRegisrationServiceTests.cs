using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxually.TechnicalTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Services.Tests
{
    [TestClass()]
    public class TaxuallyVatRegisrationServiceTests
    {
        TaxuallyVatRegisrationService? service;
        VatRegistrationRequest? request;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new TaxuallyVatRegisrationService();
            request = new VatRegistrationRequest();
        }

        [TestMethod()]
        public async Task RegisterRequestTest()
        {
            request!.Country = "FR";
            request.CompanyName = "Company";
            request.CompanyId = "12345";

            bool expected = true;
            bool actual = await service!.RegisterRequest(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task RegisterRequestBadTest()
        {
            request!.Country = "GBe";
            request.CompanyName = "Company";
            request.CompanyId = "12345";

            bool expected = false;
            bool actual = await service!.RegisterRequest(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task ApiRegisterTest()
        {
            request!.Country = "GB";
            request.CompanyName = "Company";
            request.CompanyId = "12345";

            bool expected = true;
            bool actual = await service!.ApiRegister(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task CsVRegisterTest()
        {
            request!.Country = "FR";
            request.CompanyName = "Company";
            request.CompanyId = "12345";

            bool expected = true;
            bool actual = await service!.ApiRegister(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task XmlRegisterTest()
        {
            request!.Country = "DE";
            request.CompanyName = "Company";
            request.CompanyId = "12345";

            bool expected = true;
            bool actual = await service!.ApiRegister(request);

            Assert.AreEqual(expected, actual);
        }
    }
}
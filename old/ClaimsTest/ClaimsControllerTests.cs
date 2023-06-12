using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MarkelAPI;
using Markel.Data.Model;

namespace ClaimsTest
{
    [TestFixture]
    public class ClaimsControllerTests
    {
        private TestServer _server;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Test]
        public async Task GetCompany_WithValidId_ReturnsCompanyWithInsurancePolicyStatus()
        {
            // Arrange
            var assuredName = "TestName1";
            var claim = new Claim
            {
                UCR = "5000",
                CompanyId = 1,
                ClaimDate = (DateTime.Now.AddDays(-10)).Date,
                LossDate = (DateTime.Now.AddDays(-15)).Date,
                AssuredName = assuredName,
                IncurredLoss = 1000.11M,
                Closed = false
            };
            var expectedResponse = new { Claim = claim, DaysOld = (DateTime.UtcNow - claim.ClaimDate).Days };
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(expectedResponse);
            var strExpectedResponse = jsonString.ToString().ToLower();

            // Act
            var response = await _client.GetAsync($"/api/Claims/claim/{assuredName}");
            var responseData = await response.Content.ReadAsStringAsync();
            var lowerResponseData = responseData.ToLower();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(responseData);
            Assert.AreEqual(strExpectedResponse, lowerResponseData);
        }
    }
}

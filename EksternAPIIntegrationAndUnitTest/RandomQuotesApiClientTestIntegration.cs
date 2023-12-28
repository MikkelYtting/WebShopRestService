using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace YourNamespace.Tests
{
    [TestClass]
    public class RandomQuotesApiClientIntegrationTest
    {
        private RandomQuotesApiClient _apiClient;
        private readonly string _apiKey = "08f181344fmsh5c49b56b80636eep121031jsn470b5d9190df"; // Replace with your actual API key

        [TestInitialize]
        public void Setup()
        {
            _apiClient = new RandomQuotesApiClient(new HttpClient(), _apiKey);
        }

        [TestMethod]
        public async Task GetRandomQuotesAsync_ShouldReturnQuotes()
        {
            // Arrange
            int count = 2;

            // Act
            var result = await _apiClient.GetRandomQuotesAsync(count);

            // Assert
            Assert.IsNotNull(result, "Expected non-null result from API call.");
            Assert.AreEqual(count, result.Count, $"Expected {count} quotes from API call.");
            // Additional checks can be made here to validate the structure and data of the quotes
        }
    }
}
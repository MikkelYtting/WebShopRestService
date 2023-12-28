using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourNamespace.Tests
{
    [TestClass]
    public class RandomQuotesApiClientIntegrationTest
    {
        private RandomQuotesApiClient _apiClient;
        private string _apiKey;

        [TestInitialize]public void Setup()
        {
            // Retrieve the API key from environment variables
            _apiKey = Environment.GetEnvironmentVariable("API_KEY") 
                      ?? throw new InvalidOperationException("API key not found in environment variables");

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
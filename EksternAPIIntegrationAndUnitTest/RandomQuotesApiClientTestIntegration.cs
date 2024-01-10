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
            _apiKey = Environment.GetEnvironmentVariable("API_KEY");

            // Use a default key if the environment variable is not set
            if (string.IsNullOrEmpty(_apiKey))
            {
                _apiKey = "08f181344fmsh5c49b56b80636eep121031jsn470b5d9190df";
            }

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
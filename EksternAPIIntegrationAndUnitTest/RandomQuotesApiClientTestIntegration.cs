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

        [TestInitialize]
        public void Setup()
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the API key from configuration
            _apiKey = configuration["API_KEY"] ?? throw new InvalidOperationException("API key not found in configuration");

            // Initialize the API client
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
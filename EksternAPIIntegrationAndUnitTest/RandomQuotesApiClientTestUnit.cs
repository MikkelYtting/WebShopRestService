using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace YourNamespace.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage LastRequest { get; private set; }
        public HttpResponseMessage MockResponse { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(MockResponse);
        }
    }

    [TestClass]
    public class RandomQuotesApiClientTest
    {
        private MockHttpMessageHandler _mockHandler;
        private RandomQuotesApiClient _apiClient;
        private string _apiKey = "e5f836206bmsh807465e1af9e89ep11f359jsnec5b14025056"; // API Key

        [TestInitialize]
        public void Setup()
        {
            _mockHandler = new MockHttpMessageHandler();
            var client = new HttpClient(_mockHandler);
            _apiClient = new RandomQuotesApiClient(client, _apiKey);
        }

        [TestMethod]
        public async Task GetRandomQuotesAsync_ReturnsQuotes()
        {
            // Arrange
            _mockHandler.MockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{'quote': 'Test Quote', 'author': 'Test Author'}]"),
            };

            // Act
            var result = await _apiClient.GetRandomQuotesAsync(1);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(1, result.Count, "Should return 1 quote");
            // Additional assertions can be made here based on expected structure
        }
    }

}
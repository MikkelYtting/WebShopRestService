using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class RandomQuotesApiClient
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public RandomQuotesApiClient(HttpClient client, string apiKey)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        // Ensure the API key is securely stored/fetched.
    }

    public async Task<JArray> GetRandomQuotesAsync(int count)
    {
        // Construct the request with the necessary headers and query parameters.
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://famous-quotes4.p.rapidapi.com/random?category=all&count={count}"),
            Headers = {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "famous-quotes4.p.rapidapi.com" },
            },
        };

        // Send the request and process the response.
        using (var response = await _client.SendAsync(request))
        {
            try
            {
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status indicates an error.
                var body = await response.Content.ReadAsStringAsync(); // Read the response content as a string.
                return JArray.Parse(body); // Parse the JSON string into a JArray.
            }
            catch (HttpRequestException e)
            {
                // Handle any exceptions that occurred during the request.
                // This can be replaced with more specific error handling as needed.
                throw new InvalidOperationException("Error fetching quotes from the API", e);
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RandomQuotesController : ControllerBase
{
    private readonly RandomQuotesApiClient _quotesApiClient;

    public RandomQuotesController(RandomQuotesApiClient quotesApiClient)
    {
        _quotesApiClient = quotesApiClient;
    }

    [HttpGet("GetRandomQuotes")]
    public async Task<IActionResult> GetRandomQuotes(int count)
    {
        try
        {
            var quotes = await _quotesApiClient.GetRandomQuotesAsync(count);
            return Ok(quotes);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., API call failure)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
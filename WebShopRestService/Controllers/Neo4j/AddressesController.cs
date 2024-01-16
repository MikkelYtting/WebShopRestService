using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IGraphClient _client;

        public AddressesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var addresses = await _client.Cypher.Match("(a:Address)")
                .Return(a => a.As<AddressNeo>()).ResultsAsync;

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var addresses = await _client.Cypher.Match("(d:Address)")
                .Where((AddressNeo d) => d.AddressID == id)
                .Return(d => d.As<AddressNeo>()).ResultsAsync;

            return Ok(addresses.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressNeo address)
        {
            await _client.Cypher.Create("(a:Address $address)")
                .WithParam("address", address)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddressNeo address)
        {
            await _client.Cypher.Match("(d:Address)")
                .Where((AddressNeo d) => d.AddressID == id)
                .Set("d = $address")
                .WithParam("address", address)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Address)")
                .Where((AddressNeo d) => d.AddressID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

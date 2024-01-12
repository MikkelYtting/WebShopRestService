using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGraphClient _client;

        public CustomersController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _client.Cypher.Match("(p:Customer)")
                .Return(p => p.As<CustomerNeo>()).ResultsAsync;

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customers = await _client.Cypher.Match("(d:Customer)")
                .Where((CustomerNeo d) => d.CustomerID == id)
                .Return(d => d.As<CustomerNeo>()).ResultsAsync;

            return Ok(customers.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerNeo customer)
        {
            await _client.Cypher.Create("(a:Customer $customer)")
                .WithParam("customer", customer)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerNeo customer)
        {
            await _client.Cypher.Match("(d:Customer)")
                .Where((CustomerNeo d) => d.CustomerID == id)
                .Set("d = $customer")
                .WithParam("customer", customer)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Customer)")
                .Where((CustomerNeo d) => d.CustomerID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

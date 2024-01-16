using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class OrderTablesController : ControllerBase
    {
        private readonly IGraphClient _client;

        public OrderTablesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _client.Cypher.Match("(p:Order)")
                .Return(p => p.As<OrderTableNeo>()).ResultsAsync;

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orders = await _client.Cypher.Match("(d:Order)")
                .Where((OrderTableNeo d) => d.OrderID == id)
                .Return(d => d.As<OrderTableNeo>()).ResultsAsync;

            return Ok(orders.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderTableNeo order)
        {
            await _client.Cypher.Create("(a:Order $order)")
                .WithParam("order", order)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderTableNeo order)
        {
            await _client.Cypher.Match("(d:Order)")
                .Where((OrderTableNeo d) => d.OrderID == id)
                .Set("d = $order")
                .WithParam("order", order)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Order)")
                .Where((OrderTableNeo d) => d.OrderID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

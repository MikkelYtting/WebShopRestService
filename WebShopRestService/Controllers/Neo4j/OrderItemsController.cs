using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IGraphClient _client;

        public OrderItemsController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderItems = await _client.Cypher.Match("(p:OrderItem)")
                .Return(p => p.As<OrderItemNeo>()).ResultsAsync;

            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderItems = await _client.Cypher.Match("(d:OrderItem)")
                .Where((OrderItemNeo d) => d.OrderItemID == id)
                .Return(d => d.As<OrderItemNeo>()).ResultsAsync;

            return Ok(orderItems.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemNeo orderItem)
        {
            await _client.Cypher.Create("(a:OrderItem $orderItem)")
                .WithParam("orderItem", orderItem)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderItemNeo orderItem)
        {
            await _client.Cypher.Match("(d:OrderItem)")
                .Where((OrderItemNeo d) => d.OrderItemID == id)
                .Set("d = $orderItem")
                .WithParam("orderItem", orderItem)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:OrderItem)")
                .Where((OrderItemNeo d) => d.OrderItemID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

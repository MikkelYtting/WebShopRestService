using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGraphClient _client;

        public ProductsController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var addresses = await _client.Cypher.Match("(p:Product)")
                .Return(p => p.As<ProductNeo>()).ResultsAsync;

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var addresses = await _client.Cypher.Match("(d:Product)")
                .Where((ProductNeo d) => d.ProductID == id)
                .Return(d => d.As<ProductNeo>()).ResultsAsync;

            return Ok(addresses.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductNeo product)
        {
            await _client.Cypher.Create("(a:Product $product)")
                .WithParam("product", product)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductNeo product)
        {
            await _client.Cypher.Match("(d:Product)")
                .Where((ProductNeo d) => d.ProductID == id)
                .Set("d = $product")
                .WithParam("product", product)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Product)")
                .Where((ProductNeo d) => d.ProductID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

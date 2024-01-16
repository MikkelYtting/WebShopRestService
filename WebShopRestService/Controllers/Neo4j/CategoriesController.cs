using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGraphClient _client;

        public CategoriesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _client.Cypher.Match("(p:Category)")
                .Return(p => p.As<CategoryNeo>()).ResultsAsync;

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categories = await _client.Cypher.Match("(d:Category)")
                .Where((CategoryNeo d) => d.CategoryID == id)
                .Return(d => d.As<CategoryNeo>()).ResultsAsync;

            return Ok(categories.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryNeo category)
        {
            await _client.Cypher.Create("(a:Category $category)")
                .WithParam("category", category)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryNeo category)
        {
            await _client.Cypher.Match("(d:Category)")
                .Where((CategoryNeo d) => d.CategoryID == id)
                .Set("d = $category")
                .WithParam("category", category)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Category)")
                .Where((CategoryNeo d) => d.CategoryID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers.Neo4j
{
    [Route("neo4j/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGraphClient _client;

        public RolesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _client.Cypher.Match("(p:Role)")
                .Return(p => p.As<RoleNeo>()).ResultsAsync;

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var roles = await _client.Cypher.Match("(d:Role)")
                .Where((RoleNeo d) => d.RoleID == id)
                .Return(d => d.As<RoleNeo>()).ResultsAsync;

            return Ok(roles.LastOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleNeo role)
        {
            await _client.Cypher.Create("(a:Role $role)")
                .WithParam("role", role)
                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleNeo role)
        {
            await _client.Cypher.Match("(d:Role)")
                .Where((RoleNeo d) => d.RoleID == id)
                .Set("d = $role")
                .WithParam("role", role)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Role)")
                .Where((RoleNeo d) => d.RoleID == id)
                .Delete("d")
                .ExecuteWithoutResultsAsync();
            return Ok();

        }
    }
}

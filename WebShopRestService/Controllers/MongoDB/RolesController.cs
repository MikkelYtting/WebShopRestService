using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Controllers.MongoDB
{
    [Route("mongoDB/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Repositories.MongoDB.RolesRepository _mongoDBService;

        public RolesController(Repositories.MongoDB.RolesRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<RoleMongo>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleMongo role)
        {
            await _mongoDBService.CreateAsync(role);
            return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] string name)
        {
            await _mongoDBService.UpdateAsync(id, name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Controllers.MongoDB
{
    [Route("mongoDB/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Repositories.MongoDB.CategoriesRepository _mongoDBService;

        public CategoriesController(Repositories.MongoDB.CategoriesRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<CategoryMongo>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryMongo category)
        {
            await _mongoDBService.CreateAsync(category);
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
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

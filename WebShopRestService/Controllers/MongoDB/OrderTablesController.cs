using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Controllers.MongoDB
{
    [Route("mongoDB/[controller]")]
    [ApiController]
    public class OrderTablesController : ControllerBase
    {
        private readonly Repositories.MongoDB.OrderTablesRepository _mongoDBService;

        public OrderTablesController(Repositories.MongoDB.OrderTablesRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<OrderTableMongo>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderTableMongo order)
        {
            await _mongoDBService.CreateAsync(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] decimal totalAmount)
        {
            await _mongoDBService.UpdateAsync(id, totalAmount);
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

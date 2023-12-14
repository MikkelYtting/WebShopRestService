using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Controllers.MongoDB
{
    [Route("mongoDB/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly Repositories.MongoDB.OrderItemsRepository _mongoDBService;

        public OrderItemsController(Repositories.MongoDB.OrderItemsRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<OrderItemMongo>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderItemMongo orderItem)
        {
            await _mongoDBService.CreateAsync(orderItem);
            return CreatedAtAction(nameof(Get), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] int quantity)
        {
            await _mongoDBService.UpdateAsync(id, quantity);
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

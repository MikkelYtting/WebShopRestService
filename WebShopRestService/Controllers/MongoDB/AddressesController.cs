using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Controllers.MongoDB
{
    [Route("mongoDB/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly Repositories.MongoDB.AddressesRepository _mongoDBService;

        public AddressesController(Repositories.MongoDB.AddressesRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<AddressMongo>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddressMongo address)
        {
            await _mongoDBService.CreateAsync(address);
            return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AddressMongo address)
        {
            await _mongoDBService.UpdateAsync(id, address);
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

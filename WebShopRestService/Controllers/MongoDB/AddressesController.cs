using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Models.MongoDB;
using WebShopRestService.DTO.MongoDTO;

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
        public async Task<IActionResult> Post([FromBody] AddressDTO addressDto)
        {
            // Map the DTO to the domain model (manually or by using a mapper like AutoMapper)
            var addressMongo = new AddressMongo
            {
                // Assuming Id is not set because MongoDB will auto-generate it
                Street = addressDto.Street,
                City = addressDto.City,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country
            };

            // Call the service to create the new address
            await _mongoDBService.CreateAsync(addressMongo);

            // Create a response DTO including the generated Id
            var responseDto = new AddressDTO
            {
                //Id = addressMongo.Id, // MongoDB has generated the Id at this point
                Street = addressMongo.Street,
                City = addressMongo.City,
                PostalCode = addressMongo.PostalCode,
                Country = addressMongo.Country
            };

            // Return the created address with the generated Id
            // The 'Get' method should be one that exists and can handle a GET request for a single address by Id
            return CreatedAtAction(nameof(Get), new { id = addressMongo.Id }, responseDto);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    //[Authorize] // Require authentication for all methods
    public class AddressesController : ControllerBase
    {
        private readonly AddressesManager _addressesManager;

        public AddressesController(AddressesManager addressesManager)
        {
            _addressesManager = addressesManager;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var addresses = await _addressesManager.GetAddressesAsync();

            if (addresses == null)
            {
                return NotFound("Address list is empty.");
            }

            // Assuming the AddressesManager handles authorization internally:
            return Ok(addresses);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _addressesManager.GetAddressByIdAsync(id);

            if (address == null)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest("Address ID mismatch.");
            }

            try
            {
                await _addressesManager.UpdateAddressAsync(address);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            return NoContent();
        }

        // POST: api/Addresses
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            var createdAddress = await _addressesManager.CreateAddressAsync(address);

            return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.AddressId }, createdAddress);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var deleted = await _addressesManager.DeleteAddressAsync(id);

            if (!deleted)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            return NoContent();
        }
    }
}

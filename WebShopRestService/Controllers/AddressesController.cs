using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    /// <summary>
    /// Controller responsible for managing addresses.
    /// All modifying actions are restricted to administrators.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all methods
    public class AddressesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AddressesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            if (_context.Addresses == null)
            {
                return NotFound("Address list is empty.");
            }

            // If the user is an admin, return all addresses. 
            
            if (User.IsInRole("Administrator"))
            {
                return await _context.Addresses.ToListAsync();
            }
            else
            {
                 return Forbid();
            }
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound("Address list is empty.");
            }

            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            // Assuming regular users can view individual addresses:
            return address;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")] // Only admins can update addresses
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest("Address ID mismatch.");
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound($"Address with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        [HttpPost]
        [Authorize(Roles = "Administrator")] // Only admins can create addresses
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (_context.Addresses == null)
            {
                return Problem("Address entity set is null.");
            }

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")] // Only admins can delete addresses
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound("Address entity set is null.");
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
        }
    }
}

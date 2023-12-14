using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    /// <summary>
    /// Controller responsible for managing addresses.
    /// </summary>
    [Route("sql/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AddressesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Addresses

        /// <summary>
        /// Gets a list of all addresses.
        /// </summary>
        /// <returns>A list of addresses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }

            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/5

        /// <summary>
        /// Gets a specific address by ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>The requested address.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">The ID of the address to update.</param>
        /// <param name="address">The updated address data.</param>
        /// <returns>NoContent on success, BadRequest or NotFound on failure.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            // Check if the incoming data meets validation rules defined in the Address model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors.
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses

        /// <summary>
        /// Creates a new address.
        /// </summary>
        /// <param name="address">The address to create.</param>
        /// <returns>A CreatedAtAction response with the created address or BadRequest on failure.</returns>
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'MyDbContext.Addresses' is null.");
            }

            // Check if the incoming data meets validation rules defined in the Address model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors.
            }

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5

        /// <summary>
        /// Deletes a specific address by ID.
        /// </summary>
        /// <param name="id">The ID of the address to delete.</param>
        /// <returns>NoContent on success, NotFound on failure.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
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

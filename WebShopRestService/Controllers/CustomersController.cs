using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.Managers;
using WebShopRestService.Interfaces;
using WebShopRestService.Models;
using WebShopRestService.Models.Neo4j;

namespace WebShopRestService.Controllers
{
    [Route("sql/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersManager _customersManager;
        private readonly MyDbContext _spmanager;

        public CustomersController(CustomersManager customersManager, MyDbContext ctx)
        {
            _customersManager = customersManager;
            _spmanager = ctx;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customersManager.GetAll();

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customersManager.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerDTO updatedCustomer)
        {
            if (id != updatedCustomer.CustomerId)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var existingCustomer = await _customersManager.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            // Update the properties of the existing customer
            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Phone = updatedCustomer.Phone;

            try
            {
                await _customersManager.Update(id, existingCustomer);
            }
            catch (Exception ex) // Catch more specific exception if possible
            {
                // Log the exception details
                // e.g., _logger.LogError(ex, "Error updating customer with ID {CustomerId}", id);
                return StatusCode(500, "An error occurred while updating the customer.");
            }

            return NoContent(); // You can also return Ok(existingCustomer) if you want to include the updated object in the response.
        }


        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var createdCustomer = await _customersManager.Create(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerId }, createdCustomer);
        }
        
        // POST: api/Customers/register
        [HttpPost("register")]
        public async Task<ActionResult<RegisterCustomer>> Register(RegisterCustomer customer)
        {
            var parameters = new[]
            {
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@Email", customer.Email),
                new SqlParameter("@Phone", customer.Phone),
                new SqlParameter("@Street", customer.Street),
                new SqlParameter("@City", customer.City),
                new SqlParameter("@PostalCode", customer.PostalCode),
                new SqlParameter("@Country", customer.Country),
                new SqlParameter("@Username", customer.Username),
                new SqlParameter("@Password", customer.Password)
            };

            // Execute the stored procedure
            await _spmanager.Database.ExecuteSqlRawAsync("EXEC RegisterCustomer @FirstName, @LastName, @Email, @Phone, @Street, @City, @PostalCode, @Country, @Username, @Password", parameters);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Email }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customersManager.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}

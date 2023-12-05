using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;

namespace WebShopRestService.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly MyDbContext _context;

        public CustomersRepository(MyDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Exists(int customerId)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}

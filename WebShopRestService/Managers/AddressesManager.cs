
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class AddressesManager
    {
        private readonly MyDbContext _context;

        public AddressesManager(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> Get(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task Update(int id, Address address)
        {
            _context.Entry(address).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Address> Create(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task Delete(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShopRestService.Data;
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class RolesManager
    {
        private readonly MyDbContext _context;

        public RolesManager(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> Get(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task Update(int id, Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Role> Create(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}

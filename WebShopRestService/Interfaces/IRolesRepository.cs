
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Models;

namespace WebShopRestService.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int roleId);
    }
}

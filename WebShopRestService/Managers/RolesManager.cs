
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces; // Ensure to include the namespace for IRolesRepository
using WebShopRestService.Models;

namespace WebShopRestService.Managers
{
    public class RolesManager
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesManager(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _rolesRepository.GetAllRolesAsync();
        }

        public async Task<Role> Get(int id)
        {
            return await _rolesRepository.GetRoleByIdAsync(id);
        }

        public async Task Update(int id, Role role)
        {
            await _rolesRepository.UpdateRoleAsync(role);
        }

        public async Task<Role> Create(Role role)
        {
            await _rolesRepository.AddRoleAsync(role);
            return role; // Assuming the repository handles SaveChangesAsync and returns the added entity
        }

        public async Task Delete(int id)
        {
            var role = await _rolesRepository.GetRoleByIdAsync(id);
            if (role != null)
            {
                
                await _rolesRepository.DeleteRoleAsync(role.RoleId);
            }
        }

    }
}

using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")] // Apply authorization globally to all methods in the controller
    public class RolesController : ControllerBase
    {
        private readonly RolesManager _rolesManager;

        public RolesController(RolesManager rolesManager)
        {
            _rolesManager = rolesManager;
        }

        // GET: api/Roles
        [HttpGet]
        [AllowAnonymous] // Allow anonymous access to this method
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _rolesManager.GetAll();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        [AllowAnonymous] // Allow anonymous access to this method
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _rolesManager.Get(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest();
            }

            try
            {
                await _rolesManager.Update(id, role);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoleExists(id))
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

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            var createdRole = await _rolesManager.Create(role);
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.RoleId }, createdRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _rolesManager.Get(id);
            if (role == null)
            {
                return NotFound();
            }

            await _rolesManager.Delete(id);
            return NoContent();
        }

        private async Task<bool> RoleExists(int id)
        {
            var role = await _rolesManager.Get(id);
            return role != null;
        }
    }
}

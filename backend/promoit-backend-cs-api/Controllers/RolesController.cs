using Microsoft.AspNetCore.Mvc;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var allRoles = await _roleService.GetAllRoles();
            return Ok(allRoles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var roleById = await _roleService.GetRoleById(id);
            return Ok(roleById);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDTO role)
        {
            var editedRole = await _roleService.EditRole(id, role);
            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDTO role)
        {
            var newRole = new RoleDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newRole = await _roleService.CreateRole(role);
            return CreatedAtAction("GetRole", new { id = newRole.Id}, newRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }

    }
}

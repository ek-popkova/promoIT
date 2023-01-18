using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs.Services
{
    public class RoleService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<RoleService> _logger;
        private readonly ExistsService _existsService;

        public RoleService(promo_itContext db, ILogger<RoleService> logger, ExistsService existsService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            try
            {
                return await _context.Roles.Where(x => x.StatusId == 1)
                                           .Select(x => DTOService.RoleToDTO(x))
                                           .ToListAsync();

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting roles");
                throw new Exception($"Error getting roles", exception);
            }
        }

        public async Task<RoleDTO> GetRoleById(int? id)
        {
            try
            {
                var role = await _context.Roles.Where(role => role.Id == id)
                                               .FirstOrDefaultAsync();
                if (role == null)
                {
                    throw new Exception($"Cannot find role with {id}");
                }
                return DTOService.RoleToDTO(role);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting role with ID {id}");
                throw new Exception($"Error getting role with ID {id}", exception);
            }
        }

        public async Task<RoleDTO> CreateRole(RoleDTO roleDTO)
        {
            var role = new Role
            {
                RoleName= roleDTO.RoleName,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                CreateUserId= roleDTO.CreateUserId,
                UpdateUserId= roleDTO.UpdateUserId,
                StatusId= 1
            };
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return DTOService.RoleToDTO(role);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new role");
                throw new Exception("Cannot create a role", exception);
            }
        }

        public async Task<RoleDTO> EditRole(int id, RoleDTO role)
        {
            var existingRole = await _context.Roles.FindAsync(id);

            if (existingRole == null)
            {
                throw new Exception($"There is no such role");
            }
            if (id != role.Id)
            {
                throw new Exception($"The role with the ID {id} was not found");
            }

                existingRole.RoleName = role.RoleName;
                existingRole.CreateUserId = role.CreateUserId;
                existingRole.UpdateUserId = role.UpdateUserId;
                existingRole.StatusId = role.StatusId;
                existingRole.UpdateDate = DateTime.Now;

            try
            {
               // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!_existsService.RoleExists(role.Id))
                {
                    throw new Exception($"The role with the ID {id} does not exist!");
                }
                else
                {
                    _logger.LogError(exception, $"Error editting role with ID {id}");
                    throw new Exception($"Error editting role with ID {id}", exception);
                }
            }
            return DTOService.RoleToDTO(existingRole);
        }

        public async Task<RoleDTO> DeleteRole(int id)
        {
            var existingRole = await _context.Roles.FindAsync(id);

            if (existingRole == null)
            {
                throw new Exception($"There is no such role");
            }

            existingRole.StatusId = 2;
            existingRole.UpdateDate = DateTime.Now;

            try
            {
                // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                 _logger.LogError(exception, $"Error deleting role with ID {id}");
                throw new Exception($"Error deleting role with ID {id}", exception);
            }
            return DTOService.RoleToDTO(existingRole);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs.Services
{
    public class NonProfitRepresentativesService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<NonProfitRepresentativesService> _logger;
        private readonly ExistsService _existsService;

        public NonProfitRepresentativesService(promo_itContext db, ILogger<NonProfitRepresentativesService> logger, ExistsService existsService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
        }

        public async Task<IEnumerable<NonProfitRepresentativeDTO>> GetAllNPRs()
        {
            try
            {
                return await _context.NonProfitRepresentatives.Where(x => x.StatusId == 1)
                                                              .Select(x => DTOService.NonProfitRepresentativeToDTO(x))
                                                              .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting non profit representatives");
                throw new Exception($"Error getting non profit representatives", exception);
            }
        }

        public async Task<NonProfitRepresentativeDTO> GetNPRById(int? id)
        {
            try
            {
                var npr = await _context.NonProfitRepresentatives.Where(npr => npr.Id == id)
                                                                 .FirstOrDefaultAsync();
                if (npr == null)
                {
                    throw new Exception($"Cannot find non profit representative with {id}");
                }
                return DTOService.NonProfitRepresentativeToDTO(npr);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting non profit representative with ID {id}");
                throw new Exception($"Error getting non profit representative with ID {id}", exception);
            }
        }

        public async Task<NonProfitRepresentativeDTO> CreateNPR(NonProfitRepresentativeDTO NonProfitRepresentativeDTO)
        {
            var nonProfitRepresentative = new NonProfitRepresentative
            {
                Email= NonProfitRepresentativeDTO.Email,
                OrganizationName= NonProfitRepresentativeDTO.OrganizationName,
                OrganizationLink= NonProfitRepresentativeDTO.OrganizationLink,
                UserId= NonProfitRepresentativeDTO.UserId,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                CreateUserId= NonProfitRepresentativeDTO.CreateUserId,
                UpdateUserId= NonProfitRepresentativeDTO.UpdateUserId,
                StatusId= 1
            };
            try
            {
                _context.NonProfitRepresentatives.Add(nonProfitRepresentative);
                await _context.SaveChangesAsync();
                return DTOService.NonProfitRepresentativeToDTO(nonProfitRepresentative);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new non profit representative");
                throw new Exception("Cannot create a  non profit representative", exception);
            }
        }

        public async Task<NonProfitRepresentativeDTO> EditNPR(int id, NonProfitRepresentativeDTO nonProfitRepresentativeDTO)
        {
            var existingNPR = await _context.NonProfitRepresentatives.FindAsync(id);

            if (existingNPR == null)
            {
                throw new Exception($"There is no such non profit representative");
            }
            if (id != nonProfitRepresentativeDTO.Id)
            {
                throw new Exception($"The non profit representative with the ID {id} was not found");
            }

                existingNPR.Email = nonProfitRepresentativeDTO.Email;
                existingNPR.OrganizationName = nonProfitRepresentativeDTO.OrganizationName;
                existingNPR.OrganizationLink = nonProfitRepresentativeDTO.OrganizationLink;
                existingNPR.UserId = nonProfitRepresentativeDTO.UserId;
                existingNPR.StatusId = nonProfitRepresentativeDTO.StatusId;
                existingNPR.UpdateDate = DateTime.Now;

            try
            {
               // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!_existsService.NPRExists(nonProfitRepresentativeDTO.Id))
                {
                    throw new Exception($"The non profit representative with the ID {id} does not exist!");
                }
                else
                {

                    _logger.LogError(exception, $"Error editting non profit representative with ID {id}");
                    throw new Exception($"Error editting non profit representative with ID {id}", exception);
                }
            }
            return DTOService.NonProfitRepresentativeToDTO(existingNPR);
        }

        public async Task<NonProfitRepresentativeDTO> DeleteNPR(int id)
        {
            var existingNPR = await _context.NonProfitRepresentatives.FindAsync(id);

            if (existingNPR == null)
            {
                throw new Exception($"There is no such non profit representative");
            }

            existingNPR.StatusId = 2;
            existingNPR.UpdateDate = DateTime.Now;

            try
            {
                // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                 _logger.LogError(exception, $"Error deleting non profit representative with ID {id}");
                throw new Exception($"Error deleting non profit representative with ID {id}", exception);
            }
            return DTOService.NonProfitRepresentativeToDTO(existingNPR);
        }

    }
}

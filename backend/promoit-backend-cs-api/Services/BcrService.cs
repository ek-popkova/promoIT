using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs.Services
{
    public class BcrService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<BcrService> _logger;
        private readonly ExistsService _existsService;

        public BcrService(promo_itContext db, ILogger<BcrService> logger, ExistsService existsService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
        }

        public async Task<IEnumerable<BcrDTO>> GetAllBcrs()
        {
            try
            {
                return await _context.Bcrs.Where(x => x.StatusId == 1)
                                          .Select(x => DTOService.BcrToDTO(x))
                                          .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting business company representatives");
                throw new Exception($"Error getting business company representatives", exception);
            }
        }

        public async Task<IEnumerable<object>> GetProductsByBcrId(int id)
        {
            try
            {
                return await _context.Products.Include(b => b.Bcr)
                                              .Where(x => x.BcrId == id)
                                              .Select(x => new
                                              {
                                                  x.Id,
                                                  x.Name,
                                                  x.Value,
                                                  x.Bcr.CompanyName,
                                                  x.BcrId
                                              })
                                              .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting products by BCR id {id}");
                throw new Exception($"Error getting products by BCR id {id}", exception);
            }
        }

        public async Task<BcrDTO> GetBcrById(int? id)
        {
            try
            {
                var bcr = await _context.Bcrs.Where(bcr => bcr.Id == id)
                                             .FirstOrDefaultAsync();
                if (bcr == null)
                {
                    throw new Exception($"Cannot find business company representative with {id}");
                }
                return DTOService.BcrToDTO(bcr);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting business company representative with ID {id}");
                throw new Exception($"Error getting business company representative with ID {id}", exception);
            }
        }

        public async Task<BcrDTO> CreateBcr(BcrDTO bcrDTO)
        {
            var bcr = new Bcr
            {
                CompanyName = bcrDTO.CompanyName,
                UserId = bcrDTO.UserId,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                CreateUserId= bcrDTO.CreateUserId,
                UpdateUserId= bcrDTO.UpdateUserId,
                StatusId= 1
            };
            try
            {
                _context.Bcrs.Add(bcr);
                await _context.SaveChangesAsync();
                return DTOService.BcrToDTO(bcr);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new business company representative");
                throw new Exception("Cannot create a business company representative", exception);
            }
        }

        public async Task<BcrDTO> EditBcr(int id, BcrDTO bcr)
        {
            var existingBcr = await _context.Bcrs.FindAsync(id);

            if (existingBcr == null)
            {
                throw new Exception($"There is no such business company representative");
            }
            if (id != bcr.Id)
            {
                throw new Exception($"The business company representative with the ID {id} was not found");
            }

                existingBcr.CompanyName = bcr.CompanyName;
                existingBcr.UserId = bcr.UserId;
                existingBcr.UpdateUserId = bcr.UpdateUserId;
                existingBcr.StatusId = bcr.StatusId;
                existingBcr.UpdateDate = DateTime.Now;

            try
            {
               // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!_existsService.BcrExists(bcr.Id))
                {
                    throw new Exception($"The business company representative with the ID {id} does not exist!");
                }
                else
                {

                    _logger.LogError(exception, $"Error editting business company representative with ID {id}");

                    throw new Exception($"Error editting business company representative with ID {id}", exception);
                }
            }
            return DTOService.BcrToDTO(existingBcr);
        }

        public async Task<BcrDTO> DeleteBcr(int id)
        {
            var existingBcr = await _context.Bcrs.FindAsync(id);

            if (existingBcr == null)
            {
                throw new Exception($"There is no such business company representative");
            }

            existingBcr.StatusId = 2;
            existingBcr.UpdateDate = DateTime.Now;

            try
            {
                // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                 _logger.LogError(exception, $"Error deleting business company representative with ID {id}");

                throw new Exception($"Error deleting business company representative with ID {id}", exception);
            }
            return DTOService.BcrToDTO(existingBcr);
        }

    }
}

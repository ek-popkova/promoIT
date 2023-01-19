using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs.Services
{
    public class CampaignService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<CampaignService> _logger;
        private readonly ExistsService _existsService;

        public CampaignService(promo_itContext db, ILogger<CampaignService> logger, ExistsService existsService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
        }

        public async Task<IEnumerable<CampaignDTO>> GetAllCampaigns()
        {
            try
            {
                return await _context.Campaigns.Where(x => x.StatusId == 1)
                                               .Select(x => DTOService.CampaignToDTO(x))
                                               .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting campaigns");
                throw new Exception($"Error getting campaigns", exception);
            }
        }

        public async Task<CampaignDTO> GetCampaignById(int? id)
        {
            try
            {
                var campaign = await _context.Campaigns.Where(campaign => campaign.Id == id)
                                                       .FirstOrDefaultAsync();
                if (campaign == null)
                {
                    throw new Exception($"Cannot find campaign with {id}");
                }
                return DTOService.CampaignToDTO(campaign);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting campaign with ID {id}");
                throw new Exception($"Error getting campaign with ID {id}", exception);
            }
        }

        public async Task<IEnumerable<CampaignDTO>> GetCampaignsByNPCRId(int npr_id)
        {
            try
            {
                return await _context.Campaigns.Where(x => x.StatusId == 1 && x.NprId == npr_id)
                                               .Select(x => DTOService.CampaignToDTO(x))
                                               .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting campaigns");
                throw new Exception($"Error getting campaigns", exception);
            }
        }


        public async Task<IEnumerable<object>> GetCampaignsWithNPR()
        {
            var listofCampaignsWithNPR = new List<object>();
            try
            {
                var listOfCampaigns = await _context.Campaigns.Include(c => c.Npr)
                                                              .Select(c => new
                                                              {
                                                                  c.Id,
                                                                  c.Name,
                                                                  c.Link,
                                                                  c.Hashtag,
                                                                  NPR_Id = c.Npr.Id,
                                                                  NPR_email = c.Npr.Email,
                                                                  OrganizationName = c.Npr.OrganizationName,
                                                                  OrganizationLink = c.Npr.OrganizationLink
                                                              })
                                                              .ToListAsync();
                if (listOfCampaigns == null)
                {
                    throw new Exception($"Cannot find any campaigns");
                }
                foreach (var campaign in listOfCampaigns)
                {
                    listofCampaignsWithNPR.Add(campaign);
                }
                return listofCampaignsWithNPR;
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"Error getting campaigns with NPR");
                throw new Exception($"Error getting campaigns with NPR", exception);
            }
        }

        public async Task<CampaignDTO> CreateCampaign(CampaignDTO CampaignDTO)
        {
            var campaign = new Campaign
            {
                Name= CampaignDTO.Name,
                Link = CampaignDTO.Link,
                Hashtag = CampaignDTO.Hashtag,
                NprId = CampaignDTO.NprId,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                CreateUserId= CampaignDTO.CreateUserId,
                UpdateUserId= CampaignDTO.UpdateUserId,
                StatusId= 1
            };
            try
            {
                _context.Campaigns.Add(campaign);
                await _context.SaveChangesAsync();
                return DTOService.CampaignToDTO(campaign);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new campaign");
                throw new Exception("Cannot create a campaign", exception);
            }
        }

        public async Task<CampaignDTO> EditCampain(int id, CampaignDTO campaign)
        {
            var existingCampaign = await _context.Campaigns.FindAsync(id);

            if (existingCampaign == null)
            {
                throw new Exception($"There is no such campaign");
            }
            if (id != campaign.Id)
            {
                throw new Exception($"The campaign with the ID {id} was not found");
            }

                existingCampaign.Name = campaign.Name;
                existingCampaign.Link = campaign.Link;
                existingCampaign.Hashtag = campaign.Hashtag;
                existingCampaign.NprId = campaign.NprId;
                existingCampaign.UpdateUserId = campaign.UpdateUserId;
                existingCampaign.StatusId = campaign.StatusId;
                existingCampaign.UpdateDate = DateTime.Now;

            try
            {
               // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!_existsService.CampaignExists(campaign.Id))
                {
                    throw new Exception($"The campaign with the ID {id} does not exist!");
                }
                else
                {

                    _logger.LogError(exception, $"Error editting campaign with ID {id}");

                    throw new Exception($"Error editting campaign with ID {id}", exception);
                }
            }
            return DTOService.CampaignToDTO(existingCampaign);
        }

        public async Task<CampaignDTO> DeleteCampaign(int id)
        {
            var existingCampaign = await _context.Campaigns.FindAsync(id);

            if (existingCampaign == null)
            {
                throw new Exception($"There is no such campaign");
            }

            existingCampaign.StatusId = 2;
            existingCampaign.UpdateDate = DateTime.Now;

            try
            {
                // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                 _logger.LogError(exception, $"Error deleting campaign with ID {id}");

                throw new Exception($"Error deleting campaign with ID {id}", exception);
            }
            return DTOService.CampaignToDTO(existingCampaign);
        }

    }
}

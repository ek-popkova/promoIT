using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;
using System.Data.Common;

namespace promoit_backend_cs_api.Services
{
	public class SaToCampaignService
	{
		private readonly promo_itContext _context;
		private readonly ILogger<SaToCampaignService> _logger;

		public SaToCampaignService(promo_itContext db, ILogger<SaToCampaignService> logger)
		{
			_context = db;
			_logger = logger;
		}

		public async Task<object> GetSocialActToCampaignWithCampaignInfoBySocialActId(int id)
		{
			var listCampaignsAndMoneyBySocialActId = new List<object>();

			try
			{
				var listCampaignsAndMoney = await _context.SpResults.FromSqlInterpolated($"[promoit].[dbo].[GetSocialActToCampaign] @__id_0={id}").ToListAsync();

				if (listCampaignsAndMoney == null)
				{
					throw new Exception($"Cannot find any campaigns with social activists ID {id}");
				}
				foreach (var cap in listCampaignsAndMoney)
				{
					listCampaignsAndMoneyBySocialActId.Add(cap);
				}
				return listCampaignsAndMoneyBySocialActId;

			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error getting campaigns and money with social activist ID {id}");
				throw new Exception($"\"Error getting campaigns and money with social activist ID {id}", exception);
			}
		}

/*		public async Task<SaToCampaignDTO> UpdateSaToCampaign(int id, SaToCampaignDTO saToCampaignDTO)
		{
			var existingSaToCampaign = await _context.SaToCampaigns.Where(s => s.Id == id).FirstOrDefaultAsync<SaToCampaign>();

			if (existingSaToCampaign == null)
			{
                throw new Exception($"There is no such social activist or campaign");
            }
            if (id != saToCampaignDTO.Id)
            {
                throw new Exception($"The SaToCampaign with the ID {id} was not found");
            }
            existingSaToCampaign.SocialActivistId = saToCampaignDTO.SocialActivistId;
			existingSaToCampaign.CampaignId = saToCampaignDTO.CampaignId;
			existingSaToCampaign.Money = saToCampaignDTO.Money;
			existingSaToCampaign.UpdateDate = DateTime.Now;
			existingSaToCampaign.UpdateUserId = saToCampaignDTO.UpdateUserId;

			try
			{
				await _context.SaveChangesAsync();
			}
            catch (DbUpdateConcurrencyException exception)
            {
                    _logger.LogError(exception, $"Error editting SaToCampaign with ID {id}");

                    throw new Exception($"Error editting product with ID {id}", exception);
            }
			return DTOService.SaToCampaignDTO(existingSaToCampaign);

        }*/

	}
}

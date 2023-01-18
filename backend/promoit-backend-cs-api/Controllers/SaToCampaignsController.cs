using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;
using promoit_backend_cs_api.Services;

namespace promoit_backend_cs_api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SaToCampaignsController : ControllerBase
	{
		private readonly SaToCampaignService _saToCampaignService;
		private readonly promo_itContext _context;

		public SaToCampaignsController(SaToCampaignService saToCampaignService, promo_itContext db)
		{
			_saToCampaignService = saToCampaignService;
			_context = db;
		}

		[HttpGet("/api/SaToCampaignWithCampaignInfo/{id}")]
		public async Task<ActionResult<SaToCampaign>> GetSocialActToCampaignWithCampaignInfoBySocialActId(int id)
		{
			var socialActoWithCampaign = await _saToCampaignService.GetSocialActToCampaignWithCampaignInfoBySocialActId(id);
			return Ok(socialActoWithCampaign);
		}

/*		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSaToCampaign(int id, SaToCampaignDTO saToCampaignDTO)
		{
			var editSaToCampaign = await _saToCampaignService.UpdateSaToCampaign(id, saToCampaignDTO);
			return NoContent();
		}*/

	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly CampaignService _campaignService;

        public CampaignsController(CampaignService campaignService)
        {
            _campaignService= campaignService;
        }

        // GET: api/Campaigns
        [HttpGet]
		[Authorize(Roles = "Social activist, Admin")]
		public async Task<ActionResult<IEnumerable<CampaignDTO>>> GetCampaigns()
        {
            var allCampaigns = await _campaignService.GetAllCampaigns();
            return Ok(allCampaigns);
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign(int id)
        {
            var campaign = await _campaignService.GetCampaignById(id);
            return Ok(campaign);
        }

        [HttpGet("/api/Campaigns/CampaignsWithNPR")]
		[Authorize(Roles = "Business company representative, Admin")]
		public async Task<ActionResult<object>> GetCampaignsWithNPR()
        {
            var listOfCampaignsWithNPR = await _campaignService.GetCampaignsWithNPR();
            return Ok(listOfCampaignsWithNPR);
        }

        // PUT: api/Campaigns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(int id, CampaignDTO campaign)
        {
            await _campaignService.EditCampain(id, campaign);
            return Ok();
        }

        // POST: api/Campaigns
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignDTO campaign)
        {
            var newCampaign = new CampaignDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newCampaign = await _campaignService.CreateCampaign(campaign);
            return CreatedAtAction("GetCampaign", new { id = campaign.Id }, newCampaign);
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            await _campaignService.DeleteCampaign(id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonProfitRepresentativesController : ControllerBase
    {
        private readonly NonProfitRepresentativesService _nonProfitRepresentativeService;

        public NonProfitRepresentativesController(NonProfitRepresentativesService nonProfitRepresentativesService)
        {
            _nonProfitRepresentativeService = nonProfitRepresentativesService;
        }

        // GET: api/NonProfitRepresentatives
        [HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<NonProfitRepresentative>>> GetNonProfitRepresentatives()
        {
            var allNPRS = await _nonProfitRepresentativeService.GetAllNPRs();
            return Ok(allNPRS);
        }

        // GET: api/NonProfitRepresentatives/5
        [HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<NonProfitRepresentative>> GetNonProfitRepresentative(int id)
        {
            var nprById = await _nonProfitRepresentativeService.GetNPRById(id);
            return Ok(nprById);
        }

        [HttpGet("/api/NonProfitRepresentatives/NpcrIdByUserId/{user_id}")]
		[Authorize(Roles = "Non-profit company representative, Admin")]
		public async Task<ActionResult<int>> GetNpcrIdByUserId(string user_id)
        {
            var id = await _nonProfitRepresentativeService.GetNpcrIdByUserId(user_id);
            return Ok(id);
        }

        // PUT: api/NonProfitRepresentatives/5
        [HttpPut("{id}")]
		[Authorize(Roles = " Admin")]
		public async Task<IActionResult> PutNonProfitRepresentative(int id, NonProfitRepresentativeDTO nonProfitRepresentative)
        {
            await _nonProfitRepresentativeService.EditNPR(id, nonProfitRepresentative);
            return Ok();
        }

        // POST: api/NonProfitRepresentatives
        [HttpPost]
        public async Task<ActionResult<NonProfitRepresentative>> PostNonProfitRepresentative(NonProfitRepresentativeDTO nonProfitRepresentative)
        {
            var newNPR = new NonProfitRepresentativeDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newNPR = await _nonProfitRepresentativeService.CreateNPR(nonProfitRepresentative);
            return CreatedAtAction("GetNonProfitRepresentative", new { id = newNPR.Id }, newNPR);
        }

        // DELETE: api/NonProfitRepresentatives/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteNonProfitRepresentative(int id)
        {
            await _nonProfitRepresentativeService.DeleteNPR(id);
            return Ok();
        }

    }
}

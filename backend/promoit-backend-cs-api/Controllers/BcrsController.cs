using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BcrsController : ControllerBase
    {
        private readonly BcrService _bcrService;

        public BcrsController(BcrService bcrService)
        {
            _bcrService = bcrService;
        }

        // GET: api/Bcrs
        [HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<Bcr>>> GetBcrs()
        {
            var allBcrs =  await _bcrService.GetAllBcrs();
            return Ok(allBcrs);
        }

        [HttpGet("/api/Bcrs/productByBcrId/{id}")]
		[Authorize(Roles = "Business company representative, Admin")]
		public async Task<ActionResult<IEnumerable<object>>> GetProductsByBcrId(int id)
        {
            var productsByBcrId = await _bcrService.GetProductsByBcrId(id);
            return Ok(productsByBcrId);
        }

        // GET: api/Bcrs/5
        [HttpGet("{id}")]
		[Authorize(Roles = "Business company representative, Admin")]
		public async Task<ActionResult<Bcr>> GetBcrById(int id)
        {
            var bcrById = await _bcrService.GetBcrById(id);
            return Ok(bcrById);
        }

        [HttpGet("/api/Bcrs/BcrIdByUserId/{user_id}")]

		[Authorize(Roles = "Business company representative, Admin")]
		public async Task<ActionResult<int>> GetBcrIdByUserId(string user_id)

        {
            var id = await _bcrService.GetBcrIdByUserId(user_id);
            return Ok(id);
        }

        // PUT: api/Bcrs/5
        [HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PutBcr(int id, BcrDTO bcr)
        {
            var editedBcr = await _bcrService.EditBcr(id, bcr);
            return NoContent();
        }

        // POST: api/Bcrs
        [HttpPost]
        public async Task<ActionResult<Bcr>> PostBcr(BcrDTO bcr)
        {
            var newBcr = new BcrDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newBcr = await _bcrService.CreateBcr(bcr);
            return CreatedAtAction("GetBcrById", new { id = newBcr.Id }, newBcr);
        }

        // DELETE: api/Bcrs/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteBcr(int id)
        {
            await _bcrService.DeleteBcr(id);
            return NoContent();
        }

    }
}

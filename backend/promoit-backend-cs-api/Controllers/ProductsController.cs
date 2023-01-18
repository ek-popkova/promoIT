using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using promoit_backend_cs.Services;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;
using Shared;

namespace promoit_backend_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var allProducts = await _productService.GetAllProducts();
            return Ok(allProducts);
        }
/*        [HttpGet("/api/ProductsWithBr")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsWithBCR()
        {
            var allProductsWithBCR = await _productService.GetAllProductsWithBcr();
            return Ok(allProductsWithBCR);
        }*/

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productById = await _productService.GetProductById(id);
            return Ok(productById);
        }

        [HttpGet("/api/allProductsToCampaigns")]
		[Authorize(Roles = "Business company representative, Admin")]
		public async Task<ActionResult<IEnumerable<ProductToCampaignDTO>>> GetAllProductsToCampaigns()
        {
            var products = await _productService.GetAllProductsToCampaigns();
            return Ok(products);

		}

        [HttpGet("/api/ProductToCampaignByProductId/{id}")]
        public async Task<ActionResult<ProductToCampaign>> GetProductToCampaignByProductId(int id)
        {
            var productToCampaignByProductId = await _productService.GetProductsToCampaignByProductId(id);
            return Ok(productToCampaignByProductId);
        }

        [HttpGet("/api/ProductToCampaignByCampaignId/{id}")]
        public async Task<ActionResult<ProductToCampaign>> GetProductToCampaignByCampaignId(int id)
        {
            var productToCampaignByCampaignId = await _productService.GetProductsToCampaignByCampaignIdWithCampaignInfo(id);
            return Ok(productToCampaignByCampaignId);
        }

        [HttpGet("/api/ProductToCampaignInfo")]
		[Authorize(Roles = "Social activist, Admin")]

		public async Task<ActionResult<object>> GetProductToCampaignInfo()
        {
            var productsAndCampaigns = await _productService.GetAllProductsAndCampaigns();
            return Ok(productsAndCampaigns);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            var editedProduct = await _productService.EditProduct(id, product);
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
        {
            var newProduct = new ProductDTO();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newProduct = await _productService.CreateProduct(product);
            return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpPost("/api/PostProductToCampaign")]
		[Authorize(Roles = "Business company representative, Social activist, Admin")]
		public async Task<ActionResult<ProductToCampaign>> PostProductToCampaign(ProductToCampaignDTOShared productToCampaignDTO)
        {
            var newProductToCampaign = new ProductToCampaignDTO();
            if (ModelState.IsValid)
            {
                newProductToCampaign = await _productService.AddProductToCampaign(productToCampaignDTO);
            }
            return CreatedAtAction("GetProductToCampaignByProductId", new { id = newProductToCampaign.ProductId}, newProductToCampaign);
        }

        [HttpPut("/api/PutProductToCampaign/{id}")]
		[Authorize(Roles = "Business company representative, Social activist, Admin")]
		public async Task<IActionResult> PutProductToCampaign(int id, ProductToCampaignDTOShared productToCampaign)
        {
            var editedProductToCampaign = await _productService.EditProductToCampaign(id, productToCampaign);
            return NoContent();
        }


        [HttpPost("/api/PostProductAndCampaign")]
        public async Task<ActionResult<ProductToCampaign>> PostProductAndCampaign(string product_name, int product_value, int Bcr_id, int initial_number, int campaign_id, int user_id)
        {
            var newProduct = new ProductDTO();
            var resultProduct = new ProductDTO();

            newProduct.BcrId = Bcr_id;
            newProduct.Name = product_name;
            newProduct.Value = product_value;
            newProduct.UpdateUserId = user_id;
            newProduct.CreateUserId = user_id;


            resultProduct = await _productService.CreateProduct(newProduct);

            var newProductToCampaign = new ProductToCampaignDTOShared();
            var resultProductToCampaign = new ProductToCampaignDTO();

            newProductToCampaign.ProductId = resultProduct.Id;
            newProductToCampaign.CampaignId = campaign_id;
            newProductToCampaign.InititalNumber = initial_number;
            newProductToCampaign.UpdateUserId = user_id;
            newProductToCampaign.CreateUserId = user_id;

            resultProductToCampaign = await _productService.AddProductToCampaign(newProductToCampaign);

            return CreatedAtAction("GetProductToCampaignByProductId", new { id = resultProductToCampaign.ProductId }, resultProductToCampaign);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;
using Shared;
using System.Linq;

namespace promoit_backend_cs.Services
{
    public class ProductService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly ExistsService _existsService;

        public ProductService(promo_itContext db, ILogger<ProductService> logger, ExistsService existsService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            try
            {
                return await _context.Products.Where(x => x.StatusId == 1)
                                              .Select(x => DTOService.ProductToDTO(x))
                                              .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting products");
                throw new Exception($"Error getting products", exception);
            }
        }

        public async Task<IEnumerable<ProductToCampaignDTO>> GetAllProductsToCampaigns()
        {
            try
            {
                return await _context.ProductToCampaigns.Where(x => x.StatusId == 1)
                                                        .Select(x => DTOService.ProductToCampaignToDTO(x))
                                                        .ToListAsync();
            }
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error getting products and campaigns");
				throw new Exception($"Error getting products and campaigns", exception);
			}
		}

        public async Task<IEnumerable<object>> GetAllProductsAndCampaigns()
        {
            try
            {
                return await _context.ProductToCampaigns.Include(c => c.Campaign)
                                                        .Include(p => p.Product)
                                                        .Where(x => x.StatusId == 1)
                                                        .Select(x => new
                                                        {
                                                        
                                                            campaignId = x.Campaign.Id,
                                                            campaignName = x.Campaign.Name,
                                                            productName = x.Product.Name,
                                                            productValue = x.Product.Value,
                                                            productId = x.Product.Id,
                                                            BCRid = x.Product.BcrId,
                                                            x.Id,
                                                            x.InititalNumber,
                                                            x.BoughtNumber
                                                        })
                                                        .ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting products and campaigns");
                throw new Exception($"Error getting products and campaigns", exception);
            }
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.Where(product => product.BcrId == id)
                                                     .FirstOrDefaultAsync();
                if (product == null)
                {
                    throw new Exception($"Cannot find product with {id}");
                }
                return DTOService.ProductToDTO(product);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting product with ID {id}");
                throw new Exception($"Error getting product with ID {id}", exception);
            }
        }


        public async Task<IEnumerable<ProductToCampaignDTO>> GetProductsToCampaignByProductId(int id)
        {
            var listProductToCampaignByProductIdDTO = new List<ProductToCampaignDTO>();
            try
            {
                var listProductToCampaignByProductId = await _context.ProductToCampaigns.Where(x => x.ProductId== id)
                                                                                        .ToListAsync();
                if (listProductToCampaignByProductId == null)
                {
                    throw new Exception($"Cannot find product with product ID {id}");
                }
                foreach (var ptc in listProductToCampaignByProductId)
                {
                    var productToCampaignDTO = (DTOService.ProductToCampaignToDTO(ptc));
                    listProductToCampaignByProductIdDTO.Add(productToCampaignDTO);

                }
                return listProductToCampaignByProductIdDTO;
            } 
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting product with product ID {id}");
                throw new Exception($"Error getting product with product ID {id}", exception);
            }
        }

        public async Task<object> GetProductsToCampaignByCampaignIdWithCampaignInfo(int id)
        {
            var listProductToCampaignByCampaignIdWithCampaignInfo = new List<object>();

            try
            {
                var listProductToCampaignByCampaignId = await _context.ProductToCampaigns.Include(c => c.Campaign)
                                                                                         .Include(p => p.Product)
                                                                                         .Where(x => x.CampaignId == id)
                                                                                         .Select(p => new
                                                                                         {
                                                                                             p.CampaignId,
                                                                                             p.ProductId,
                                                                                             p.InititalNumber,
                                                                                             p.BoughtNumber,
                                                                                             campaignName = p.Campaign.Name,
                                                                                             productName = p.Product.Name,
                                                                                             productValue = p.Product.Value
                                                                                         })
                                                                                         .ToListAsync();

                if (listProductToCampaignByCampaignId == null)
                {
                    throw new Exception($"Cannot find product with campaign ID {id}");
                }
                foreach (var ptc in listProductToCampaignByCampaignId)
                {
                    listProductToCampaignByCampaignIdWithCampaignInfo.Add(ptc);
                }
                return listProductToCampaignByCampaignIdWithCampaignInfo;

            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"Error getting product with campaign ID {id}");
                throw new Exception($"Error getting product with campaign ID {id}", exception);
            }
        }

		public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name= productDTO.Name,
                Value = productDTO.Value,
                BcrId = productDTO.BcrId,
                CreateDate= DateTime.Now,
                UpdateDate= DateTime.Now,
                CreateUserId= productDTO.CreateUserId,
                UpdateUserId= productDTO.UpdateUserId,
                StatusId= 1
            };
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return DTOService.ProductToDTO(product);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new product");
                throw new Exception("Cannot create a product", exception);
            }
        }

        public async Task<ProductToCampaignDTO> AddProductToCampaign(ProductToCampaignDTOShared productToCampaignDTO)
        {

            var productToCampaign = new ProductToCampaign
            {
                CampaignId = productToCampaignDTO.CampaignId,
                ProductId = productToCampaignDTO.ProductId,
                InititalNumber = productToCampaignDTO.InititalNumber,
                BoughtNumber = productToCampaignDTO.BoughtNumber,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
				/*                CreateUserId = productToCampaignDTO.CreateUserId,
								UpdateUserId = productToCampaignDTO.UpdateUserId,*/
				CreateUserId = 2,
				UpdateUserId = 5,
				StatusId = 1,
                
            };

            try
            {
                _context.ProductToCampaigns.Add(productToCampaign);
                await _context.SaveChangesAsync();
                return DTOService.ProductToCampaignToDTO(productToCampaign);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating a new product to campaign");
                throw new Exception("Cannot create a product to campaign", exception);
            }
        }

        public async Task<ProductToCampaignDTO> EditProductToCampaign(int id, ProductToCampaignDTOShared productToCampaignDTO)
        {
            var existingPTC = await _context.ProductToCampaigns.FindAsync(id);

            if (existingPTC == null)
            {
                throw new Exception($"There is no such product to campaign");
            }
/*            if (id != productToCampaignDTO.Id)
            {
                throw new Exception($"The product to campaign with the ID {id} was not found");
            }*/

            existingPTC.CampaignId = productToCampaignDTO.CampaignId;
            existingPTC.ProductId= productToCampaignDTO.ProductId;
            existingPTC.InititalNumber = productToCampaignDTO.InititalNumber;
            existingPTC.BoughtNumber = productToCampaignDTO.BoughtNumber;
            existingPTC.UpdateDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception exception)
            {
                if (!_existsService.ProductToCampaignExists(productToCampaignDTO.Id))
                {
                    throw new Exception($"The product to campaign with the ID {id} does not exist!");
                }
                else
                {

                    _logger.LogError(exception, $"Error editting product to campaign with ID {id}");
                    throw new Exception($"Error editting product to campaign with ID {id}", exception);
                }
            }
            return DTOService.ProductToCampaignToDTO(existingPTC);
        }


        public async Task<ProductDTO> EditProduct(int id, ProductDTO product)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new Exception($"There is no such product");
            }
            if (id != product.Id)
            {
                throw new Exception($"The product with the ID {id} was not found");
            }

                existingProduct.Name = product.Name;
                existingProduct.Value = product.Value;
                existingProduct.BcrId = product.BcrId;
                existingProduct.UpdateUserId = product.UpdateUserId;
                existingProduct.StatusId = product.StatusId;
                existingProduct.UpdateDate = DateTime.Now;

            try
            {
               // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!_existsService.ProductExists(product.Id))
                {
                    throw new Exception($"The product with the ID {id} does not exist!");
                }
                else
                {

                    _logger.LogError(exception, $"Error editting product with ID {id}");
                    throw new Exception($"Error editting product with ID {id}", exception);
                }
            }
            return DTOService.ProductToDTO(existingProduct);
        }

        public async Task<ProductDTO> DeleteProduct(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new Exception($"There is no such product");
            }

            existingProduct.StatusId = 2;
            existingProduct.UpdateDate = DateTime.Now;

            try
            {
                // _context.Update(existingUser);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException exception)
            {
                 _logger.LogError(exception, $"Error deleting product with ID {id}");
                throw new Exception($"Error deleting product with ID {id}", exception);
            }
            return DTOService.ProductToDTO(existingProduct);
        }
    }
}

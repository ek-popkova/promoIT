using Microsoft.EntityFrameworkCore;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.Models;
using promoit_backend_cs_api.ModelsDTO;
using promoit_backend_cs_api.Services;
using Shared;
using Newtonsoft.Json;
using System.Linq;
using System.Data.SqlTypes;

namespace promoit_backend_cs.Services
{
    public class ProductService
    {

        private readonly promo_itContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly ExistsService _existsService;
        private readonly SocialActivistService _socialActivistService;
        private readonly SaToCampaignService _saToCampaignService;



        public ProductService(promo_itContext db, ILogger<ProductService> logger, ExistsService existsService, SocialActivistService socialActivistService)
        {
            _context = db;
            _logger = logger;
            _existsService = existsService;
            _socialActivistService = socialActivistService;
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
                                                            Hashtag = x.Campaign.Hashtag,
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


        public async Task<ProductToCampaignDTO> AnalizeAndAddAllProductsAndCampaigns(ProductToCampaignDTOShared product)
        {
            var allProductsAndCampaigns = await GetAllProductsToCampaigns();
            ProductToCampaignDTO newProductToCampaign;
            if (allProductsAndCampaigns.Any(x => x.ProductId == product.ProductId && x.CampaignId == product.CampaignId))
            {
                var pac = allProductsAndCampaigns.Where(x => x.ProductId == product.ProductId && x.CampaignId == product.CampaignId)
                                                    .FirstOrDefault();

                pac.UpdateUserId = product.UpdateUserId;
                pac.InititalNumber += product.InititalNumber;
                newProductToCampaign = await EditProductToCampaign(pac.Id, pac);
            }
            else
            {
                newProductToCampaign = await AddProductToCampaign(product);
            }
            return newProductToCampaign;
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
                CreateUserId = productToCampaignDTO.CreateUserId,
                UpdateUserId = productToCampaignDTO.UpdateUserId,
				StatusId = 1
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

        public async Task<ProductToCampaignDTO> EditProductToCampaign(int id, ProductToCampaignDTO productToCampaignDTO)
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
            existingPTC.UpdateUserId= productToCampaignDTO.UpdateUserId;
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

        public async Task<ProductToCampaignDTO> AnalyzeProductToCampaignAndDonate(string user_id, string campaignName, int boughtNumber, ProductsAndCampaignsShared productAndCampaign)
        {
            int sa_id = await _socialActivistService.GetSocialActivistById(user_id);

            //уменьшить деньги активиста !! обработать отрицательные деньги
            int money = await _socialActivistService.GetMoneyByIds(sa_id, productAndCampaign.campaignId);
            SaToCampaignShared saToCampaign = new SaToCampaignShared()
            {
                social_activist_id = sa_id,
                campaign_id = productAndCampaign.campaignId,
                money = money - productAndCampaign.productValue * boughtNumber
            };
            await _socialActivistService.UpdateMoneyBackend(saToCampaign);

            //увеличить boughtnumber старой кампании !! обработать отрицательные товары
            ProductToCampaignDTO oldProductToCampaign = DTOService.ProductToCampaignToDTO(_context.ProductToCampaigns.Where(x => x.CampaignId == productAndCampaign.campaignId && x.ProductId == productAndCampaign.productId).FirstOrDefault());
            {
                oldProductToCampaign.BoughtNumber = oldProductToCampaign.BoughtNumber + boughtNumber;
                oldProductToCampaign.UpdateUserId = user_id;
                return await EditProductToCampaign(oldProductToCampaign.Id, oldProductToCampaign);
            }

            //добавить запись в новую кампанию (куда донатим или обновить старую, если уже была)
            CampaignDTO chosenCampaign = DTOService.CampaignToDTO(_context.Campaigns.Where(x => x.StatusId == 1 && x.Name == campaignName).FirstOrDefault());
            ProductToCampaignDTO? productToCampaign = DTOService.ProductToCampaignToDTO(_context.ProductToCampaigns.Where(x => x.CampaignId == chosenCampaign.Id && x.ProductId == productAndCampaign.productId).FirstOrDefault());
            if (productToCampaign is not null)
            {
                productToCampaign.InititalNumber = productToCampaign.InititalNumber + boughtNumber;
                productToCampaign.UpdateUserId = user_id;
                return await EditProductToCampaign(productToCampaign.Id, productToCampaign);
            }
            else
            {
                ProductToCampaignDTOShared newProductToCampaign = new ProductToCampaignDTOShared
                {
                    CampaignId = chosenCampaign.Id,
                    ProductId = productAndCampaign.productId,
                    InititalNumber = boughtNumber,
                    BoughtNumber = 0,
                    CreateUserId = user_id,
                    UpdateUserId = user_id,
                };
                return await AddProductToCampaign(newProductToCampaign);
            }

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

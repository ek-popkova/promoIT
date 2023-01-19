using Newtonsoft.Json;
using Shared;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class CampaignService
    {
		private readonly ILogger<CampaignService> _logger;
		private readonly HttpClient _http;
        public CampaignService(HttpClient Http, ILogger<CampaignService> logger)
        {
            _http = Http;
			_logger = logger;
        }

        public async Task<IEnumerable<CampaignShared>> GetAllCampaigns()
        {
            try
            {
                var response = await _http.GetAsync("https://localhost:7263/api/Campaigns");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CampaignShared>>(json);
			}
			catch (NullReferenceException exception)
			{
				_logger.LogError(exception, $"Error getting campaigns");
				throw new Exception($"Error getting campaigns", exception);
			}
		}

        public async Task<IEnumerable<CampaignDTO>> GetCampaignsByNPCRId(int npr_id)
        {
            try
            {
                var response = await _http.GetAsync($"https://localhost:7263/api/Campaigns/CampaignsByNPRId/{npr_id}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CampaignDTO>>(json);
            }
            catch (NullReferenceException exception)
            {
                _logger.LogError(exception, $"Error getting campaigns");
                throw new Exception($"Error getting campaigns", exception);
            }
        }

        public async Task<IEnumerable<ProductToCampaignDTOShared>> GetAllProductsToCampaigns()
        {
            try
            {
			    var response = await _http.GetAsync("https://localhost:7263/api/allProductsToCampaigns");
			    var json = await response.Content.ReadAsStringAsync();
			    return JsonConvert.DeserializeObject<IEnumerable<ProductToCampaignDTOShared>>(json);
			}
			catch (NullReferenceException exception)
			{
				_logger.LogError(exception, $"Error getting products and campaigns");
				throw new Exception($"Error getting products and campaigns", exception);
			}
		}


		public async Task<IEnumerable<ProductsAndCampaignsShared>> GetProductsAndCampaignsNames()
        {
            try
            {
                var response = await _http.GetAsync("https://localhost:7263/api/ProductToCampaignInfo");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ProductsAndCampaignsShared>>(json);
			}
			catch (NullReferenceException exception)
			{
				_logger.LogError(exception, $"Error getting products and campaigns");
				throw new Exception($"Error getting products and campaigns", exception);
			}
		}
        public async Task<IEnumerable<CampaignsAndNpr>> GetCampaignsAndNpr()
        {
            try
            {
                var response = await _http.GetAsync("https://localhost:7263/api/Campaigns/CampaignsWithNPR");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CampaignsAndNpr>>(json);
			}
			catch (NullReferenceException exception)
			{
				_logger.LogError(exception, $"Error getting campaigns");
				throw new Exception($"Error getting campaigns", exception);
			}
		}

        public async Task<HttpResponseMessage> PutProductToCampaign(int id, ProductToCampaignDTOShared ptc)
        {
            try
            {
                return await _http.PutAsJsonAsync($"https://localhost:7263/api/PutProductToCampaign/{id}", ptc);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error putting products and campaigns");
				throw new Exception($"Error putting products and campaigns", exception);
			}
		}

        public async Task<HttpResponseMessage> PostProductToCampaign(ProductToCampaignDTOShared ptc)
        {
            try
            {
                return await _http.PostAsJsonAsync("https://localhost:7263/api/PostProductToCampaign", ptc);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error posting products to campaigns");
				throw new Exception($"Error posting products to campaigns", exception);
			}
		}

        public async Task<HttpResponseMessage> AddNewCampaign(CampaignDTO newCampaign)
        {
            try
            {
                return await _http.PostAsJsonAsync("https://localhost:7263/api/Campaigns", newCampaign);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of new product");
                throw new Exception($"Error sending the data of new product", exception);
            }
        }

    }
}

using Newtonsoft.Json;
using Shared;
using System.Configuration;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class CampaignService
    {
		private readonly ILogger<CampaignService> _logger;
		private readonly AuthService _authservice;
		private readonly HttpClient _http;
        private readonly HttpClient clientNET;
        private readonly IConfiguration _configuration;

		public CampaignService(HttpClient Http, ILogger<CampaignService> logger, AuthService authservice, IHttpClientFactory factory, IConfiguration configuration)
        {
            _http = Http;
			_logger = logger;
			_authservice = authservice;
            clientNET = factory.CreateClient("NET_Server");
            _configuration = configuration;
        }
		public async Task<IEnumerable<CampaignShared>> GetAllCampaigns()
        {
            try
            {
				var response = await _http.GetAsync($"{clientNET.BaseAddress}api/Campaigns");
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
                var response = await _http.GetAsync($"{clientNET.BaseAddress}api/Campaigns/CampaignsByNPRId/{npr_id}");
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
			    var response = await _http.GetAsync($"{clientNET.BaseAddress}api/allProductsToCampaigns");
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
                var response = await _http.GetAsync($"{clientNET.BaseAddress}api/ProductToCampaignInfo");
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
				var response = await _http.GetAsync($"{clientNET.BaseAddress}api/Campaigns/CampaignsWithNPR");
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
                return await _http.PutAsJsonAsync($"{clientNET.BaseAddress}api/PutProductToCampaign/{id}", ptc);
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
                return await _http.PostAsJsonAsync($"{clientNET.BaseAddress}api/PostProductToCampaign", ptc);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error posting products to campaigns");
				throw new Exception($"Error posting products to campaigns", exception);
			}
		}

        public async Task<HttpResponseMessage> AnalizeAndPutProductToCampaign(ProductToCampaignDTOShared ptc)
        {
            try
            {
                return await _http.PutAsJsonAsync($"https://localhost:7263/api/ProductToCampaign/", ptc);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error putting products and campaigns");
                throw new Exception($"Error putting products and campaigns", exception);
            }
        }

        public async Task<HttpResponseMessage> AddNewCampaign(CampaignDTO newCampaign)
        {
            try
            {
                return await _http.PostAsJsonAsync($"{clientNET.BaseAddress}api/Campaigns", newCampaign);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of new product");
                throw new Exception($"Error sending the data of new product", exception);
            }
        }

    }
}

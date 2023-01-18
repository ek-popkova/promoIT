using Shared;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class SocialActivistService
    {
		private readonly ILogger<SocialActivistService> _logger;
		private readonly AuthService _authservice;
		private readonly HttpClient _http;

		public SocialActivistService(HttpClient Http, ILogger<SocialActivistService> logger, AuthService authservice)
		{
			_http = Http;
			_logger = logger;
			_authservice = authservice;
		}

		public async Task<IEnumerable<SpResults>> GetCampaignsAndMoney(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<IEnumerable<SpResults>>($"https://localhost:7263/api/SaToCampaignWithCampaignInfo/{id}");
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error getting campaigns and money");
				throw new Exception($"Error getting campaigns and money", exception);
			}
		}

        public async Task<HttpResponseMessage> UpdateMoney(int id, SaToCampaignShared saToCampaignShared)
        {
            try
            {
                return await _http.PutAsJsonAsync($"http://localhost:7000/sa-to-campaign/{id}", saToCampaignShared);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error updating money");
				throw new Exception($"Error updating money", exception);
			}
		}
    }
}

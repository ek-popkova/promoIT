using Newtonsoft.Json;
using Shared;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class NonProfitRepresentativeService
    {
        private readonly HttpClient _http;
        private readonly ILogger<NonProfitRepresentativeService> _logger;
        private readonly HttpClient clientNET;

        public NonProfitRepresentativeService(HttpClient http, ILogger<NonProfitRepresentativeService> logger, IHttpClientFactory factory)
        {
            _http = http;
            _logger = logger;
            clientNET = factory.CreateClient("NET_Server");
        }

        public async Task<HttpResponseMessage> AddNewNPCR(NonProfitRepresentativeDTO newNPCR, string user_id)
        {
            newNPCR.UserId = user_id;
            newNPCR.CreateUserId = user_id;
            newNPCR.UpdateUserId = user_id;

            try
            {
                return await _http.PostAsJsonAsync($"{clientNET.BaseAddress}api/NonProfitRepresentatives", newNPCR);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of non-profit organization representative");
                throw new Exception($"Error sending the data of non-profit organization representative", exception);
            }
        }

        public async Task<int> GetNPCRidByUserId(string user_id)
        {
            try
            {
                var response = await _http.GetAsync($"{clientNET.BaseAddress}api/NonProfitRepresentatives/NpcrIdByUserId/{user_id}");
                return Int32.Parse(await response.Content.ReadAsStringAsync());

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting the non-profit organization representative id");
                throw new Exception($"Error getting the non-profit organization representative id", exception);
            }

        }


    }
}

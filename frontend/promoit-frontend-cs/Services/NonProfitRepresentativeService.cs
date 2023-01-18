using Newtonsoft.Json;
using Shared;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class NonProfitRepresentativeService
    {
        private readonly HttpClient _http;
        private readonly ILogger<NonProfitRepresentativeService> _logger;

        public NonProfitRepresentativeService(HttpClient http, ILogger<NonProfitRepresentativeService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> AddNewNPCR(NonProfitRepresentativeDTO newNPCR, string user_id)
        {
            newNPCR.UserId = user_id;
            newNPCR.CreateUserId = user_id;
            newNPCR.UpdateUserId = user_id;

            try
            {
                return await _http.PostAsJsonAsync("https://localhost:7263/api/NonProfitRepresentatives", newNPCR);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of non-profit organization representative");
                throw new Exception($"Error sending the data of non-profit organization representative", exception);
            }
        }


    }
}

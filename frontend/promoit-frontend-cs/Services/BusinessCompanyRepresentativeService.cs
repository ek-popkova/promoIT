using Newtonsoft.Json;
using Shared;
using System;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class BusinessCompanyRepresentativeService
    {
		private readonly ILogger<BusinessCompanyRepresentativeService> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AuthService _authservice;
		private readonly HttpClient _http;

		public BusinessCompanyRepresentativeService(HttpClient http, ILogger<BusinessCompanyRepresentativeService> logger, IHttpContextAccessor HttpContextAccessor, AuthService authservice)
		{
			_http = http;
			_logger = logger;
			_httpContextAccessor = HttpContextAccessor;
			_authservice = authservice;
		}

		public async Task<IEnumerable<SaTransactionSharedSAInfo>> GetTransactionWithSAInfo(int id)
        {
			try
            {
                var response = await _http.GetAsync($"http://localhost:7000/business-company-representative/{id}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<SaTransactionSharedSAInfo>>(json);
			}
			catch (NullReferenceException exception)

			{
				_logger.LogError(exception, $"Error getting transactions");
				throw new Exception($"\"Error getting transactions", exception);
			}
		}
        public async Task<IEnumerable<ProductsAndBcrInfo>> GetProductsAndBCRInfo(int id)
        {
            try
            {
			    var response = await _http.GetAsync($"https://localhost:7263/api/Bcrs/productByBcrId/{id}");
			    var json = await response.Content.ReadAsStringAsync();
			    return JsonConvert.DeserializeObject<IEnumerable<ProductsAndBcrInfo>>(json);
			}
			catch (NullReferenceException exception)
			{
				_logger.LogError(exception, $"Error getting products");
				throw new Exception($"\"Error getting products", exception);
			}
		}

        public async Task<HttpResponseMessage> AddSocialActivistTransaction(SaTransactionShared saTransactionShared)
        {
            try
            {
                return await _http.PostAsJsonAsync("http://localhost:7000/business-company-representative", saTransactionShared);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error adding transactions");
				throw new Exception($"Error adding transactions", exception);
			}
		}

        public async Task<HttpResponseMessage> SendProductToSocialActivist(int saTransactionId)
        {
            try
            {
                return await _http.DeleteAsync($"http://localhost:7000/business-company-representative/ship/{saTransactionId}");
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error sending transactions");
				throw new Exception($"Error sending transactions", exception);
			}
		}

	}
}

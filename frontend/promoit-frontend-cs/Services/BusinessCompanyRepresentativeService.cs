﻿using Newtonsoft.Json;
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
        private readonly HttpClient clientJS;
        private readonly HttpClient clientNET;

		public BusinessCompanyRepresentativeService(HttpClient http, ILogger<BusinessCompanyRepresentativeService> logger, IHttpContextAccessor HttpContextAccessor, AuthService authservice, IHttpClientFactory factory)
		{
			_http = http;
			_logger = logger;
			_httpContextAccessor = HttpContextAccessor;
			_authservice = authservice;
            clientJS = factory.CreateClient("NodeJS_Server");
            clientNET = factory.CreateClient("NET_Server");
		}

		public async Task<IEnumerable<SaTransactionSharedSAInfo>> GetTransactionWithSAInfo(int id)
        {
			try
            {
                var response = await _http.GetAsync($"{clientJS.BaseAddress}business-company-representative/{id}");
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
			    var response = await _http.GetAsync($"{clientNET.BaseAddress}api/Bcrs/productByBcrId/{id}");
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
				return await _http.PostAsJsonAsync($"{clientJS.BaseAddress}business-company-representative", saTransactionShared);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error adding transactions");
				throw new Exception($"Error adding transactions", exception);
			}
		}

        public async Task<HttpResponseMessage> SendProductToSocialActivist(int saTransactionId, string user_id)
        {
            try
            {
                return await _http.DeleteAsync($"{clientJS.BaseAddress}business-company-representative/ship/{saTransactionId}/{user_id}");
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error sending transactions");
				throw new Exception($"Error sending transactions", exception);
			}
		}

        public async Task<HttpResponseMessage> AddNewBCR(BcrDTO newBCR, string user_id)
        {
            newBCR.UserId = user_id;
            newBCR.CreateUserId = user_id;
            newBCR.UpdateUserId = user_id;

            try
            {
                return await _http.PostAsJsonAsync($"{clientNET.BaseAddress}api/Bcrs", newBCR);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of business company representative");
                throw new Exception($"Error sending the data of business company representative", exception);
            }
        }

        public async Task<HttpResponseMessage> AddNewProduct(ProductDTO newProduct)
        {

            try
            {
                return await _http.PostAsJsonAsync($"{clientNET.BaseAddress}api/Products", newProduct);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of new product");
                throw new Exception($"Error sending the data of new product", exception);
            }
        }

        public async Task<int> GetBCRidByUserId(string user_id)
        {
            try
            {
                var response = await _http.GetAsync($"{clientNET.BaseAddress}api/Bcrs/BcrIdByUserId/{user_id}");
                return Int32.Parse(await response.Content.ReadAsStringAsync());

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting the business company representative id");
                throw new Exception($"Error getting the business company representative id", exception);
            }

        }


    }
}

using Newtonsoft.Json;
using NuGet.Common;
using promoit_frontend_cs.Pages.Admin;
using Shared;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace promoit_frontend_cs.Services
{
    public class SocialActivistService
    {
		private readonly ILogger<SocialActivistService> _logger;
		private readonly AuthService _authservice;
		private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient clientJS;
		private readonly HttpClient clientNET;

		public SocialActivistService(HttpClient Http, ILogger<SocialActivistService> logger, AuthService authservice, IHttpClientFactory factory)
		{
			_http = Http;
			_logger = logger;
			_authservice = authservice;
            clientJS = factory.CreateClient("NodeJS_Server");
			clientNET = factory.CreateClient("NET_Server");
		}

        public async Task<int> GetSocialActivistById(string id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<int>($"{clientJS.BaseAddress}social-activist-id/{id}");
                return response;
            } catch(Exception exception)
			{
				_logger.LogError(exception, $"Error getting social activist by ID {id}");
				throw new Exception($"Error getting social activist by ID {id}", exception);
			}
        }

		public async Task<IEnumerable<SpResults>> GetCampaignsAndMoney(int id)
        {
            try
			{
                return await _http.GetFromJsonAsync<IEnumerable<SpResults>>($"{clientNET.BaseAddress}api/SaToCampaignWithCampaignInfo/{id}");
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
                return await _http.PutAsJsonAsync($"{clientJS.BaseAddress}sa-to-campaign/{id}", saToCampaignShared);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Error updating money");
				throw new Exception($"Error updating money", exception);
			}
		}

        public async Task<HttpResponseMessage> AddNewSocialActivist(SocialActivistDTO newSocialActivist, string user_id)
        {
            newSocialActivist.user_id = user_id;
            newSocialActivist.create_user_id = user_id;
            newSocialActivist.update_user_id = user_id;

            try
            {
                var response = await _http.PostAsJsonAsync($"{clientJS.BaseAddress}social-activists", newSocialActivist);
                return response;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error sending the data of social activist");
                throw new Exception($"Error sending the data of social activist", exception);
            }
        }

        public async Task<SocialActivistDTO[]> GetSocialActivists()
        {
            try
            {
                var response = await _http.GetAsync($"{clientJS.BaseAddress}social-activists");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SocialActivistDTO[]>(json);
                return data;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting the data of social activist");
                throw new Exception($"Error getting the data of social activist", exception);
            }
        }

        public async Task<IEnumerable<TwitterReportType>> GetTwitterReport()
        {
            try
            {
                var response = await _http.GetAsync($"{clientJS.BaseAddress}twitter-report");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<TwitterReportType>>(json);
                return data;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error getting the twitter report");
                throw new Exception($"Error getting the twitter report", exception);
            }
        }
    }
}

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


//namespace promoit_blazor_nodejs_frontend.Shared
//{
    public class SocialActivistClass
    {
        private readonly HttpClient _httpClient;

        public SocialActivistClass(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SocialActivistDTO[]> GetSocialActivistsFromBackEnd()
        {
            try
            {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuhenticationHeaderValue("Bearer", "Your Oauth token");
            var response = await _httpClient.GetAsync("http://localhost:7000/social-activists");
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuhenticationHeaderValue("Bearer", "Your Oauth token");
               var json = await response.Content.ReadAsStringAsync();
               var data = JsonConvert.DeserializeObject<SocialActivistDTO[]>(json);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR!", ex);
            }
        }
    }
//}
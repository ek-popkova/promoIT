using NuGet.Common;
using System.Net.Http.Headers;

namespace promoit_frontend_cs.Services
{
	public class AuthService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly HttpClient _http;

		public AuthService(IHttpContextAccessor HttpContextAccessor, HttpClient http) 
		{
			_http = http;
			_httpContextAccessor = HttpContextAccessor;

			if (_httpContextAccessor.HttpContext != null )
			{
				string token = _httpContextAccessor.HttpContext.Request.Cookies["auth_token"];
				_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

	}
}

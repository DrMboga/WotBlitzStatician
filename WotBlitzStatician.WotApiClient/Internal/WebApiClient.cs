namespace WotBlitzStatician.WotApiClient
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	public class WebApiClient
	{
        private readonly ILogger<WebApiClient> _log;

		private const string Guid = "fcdda45ba2a74c2f8cc8562bbfbb7a0a";
		private readonly IProxySettings _proxySettings;

		public WebApiClient(IProxySettings proxySettings, ILogger<WebApiClient> logger)
		{
			_proxySettings = proxySettings;
			_log = logger;
		}

		public async Task<TResponse> GetResponse<TResponse>(string baseAddress, string request, bool isPostNeeded = false)
		{
			var handler = new HttpClientHandler();
			if (_proxySettings.UseProxy)
			{
				handler.DefaultProxyCredentials = new NetworkCredential(
					_proxySettings.User,
					_proxySettings.PwdHash.DecryptString(Guid),
					_proxySettings.Domain);
			}

			using (handler)
			using (var client = new HttpClient(handler))
			{
				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response;
				if(isPostNeeded)
				{
					// Create httpContent
					var requestBody = GetBody(request);
					var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

					response = await client.PostAsync(request, httpContent);
				}
				else
				{
					response = await client.GetAsync(request);
				}

                _log.LogInformation($"Request '{baseAddress}{request}' - Status: '{response.StatusCode}'");

				response.EnsureSuccessStatusCode();
				var responseString = await response.Content.ReadAsStringAsync();

				var responseBody = JsonConvert.DeserializeObject<ResponseBody<TResponse>>(responseString);

				if (responseBody.Status == "ok")
				{
					return responseBody.Data;
				}
				if (responseBody.Status == "error")
				{
					var error = responseBody.Error;
					var message = $"Field:{error.Field}  Message:{error.Message}  Value:{error.Value}  Code:{error.Code}";
					throw new ResponseException(message)
					{
						Error = error,
					};
				}
				return default(TResponse);

			}
		}

		private string GetBody(string request)
		{
			return request.Substring(request.IndexOf('?') + 1);
		}
	}
}
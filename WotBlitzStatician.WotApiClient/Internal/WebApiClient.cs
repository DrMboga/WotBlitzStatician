namespace WotBlitzStatician.WotApiClient
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
    using log4net;
    using Newtonsoft.Json;

	internal class WebApiClient
	{
        private static readonly ILog _log = LogManager.GetLogger(typeof(WebApiClient));

		private const string Guid = "fcdda45ba2a74c2f8cc8562bbfbb7a0a";
		private readonly IProxySettings _proxySettings;

		public WebApiClient(IProxySettings proxySettings)
		{
			_proxySettings = proxySettings;
		}

		public async Task<TResponse> GetResponse<TResponse>(string baseAddress, string request)
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

				var response = await client.GetAsync(request);

                _log.Debug($"Request '{baseAddress}{request}' - Status: '{response.StatusCode}'");

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

	}
}
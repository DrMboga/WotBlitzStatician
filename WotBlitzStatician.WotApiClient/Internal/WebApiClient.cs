namespace WotBlitzStatician.WotApiClient
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using Newtonsoft.Json;

	internal class WebApiClient
	{
		public async Task<TResponse> GetResponse<TResponse>(string baseAddress, string request)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(request);
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
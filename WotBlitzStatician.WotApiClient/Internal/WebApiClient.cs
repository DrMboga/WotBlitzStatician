namespace WotBlitzStatician.WotApiClient
{
  using System;
  using System.Diagnostics;
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
    private readonly IWgApiConfiguration _wgApiConfiguration;

    public WebApiClient(
			IProxySettings proxySettings, 
			IWgApiConfiguration wgApiConfiguration,
			ILogger<WebApiClient> logger)
    {
      _proxySettings = proxySettings;
      _wgApiConfiguration = wgApiConfiguration;
      _log = logger;
    }

    public async Task<TResponse> GetResponse<TResponse>(string baseAddress, string request, bool isPostNeeded = false)
    {
      var handler = new HttpClientHandler();
      if (_proxySettings.UseProxy)
      {
        var proxy = new WebProxy(_proxySettings.ProxyAddress, true);
        proxy.Credentials = new NetworkCredential(
          _proxySettings.User,
          _proxySettings.PwdHash.DecryptString(Guid),
          _proxySettings.Domain);
        handler.Proxy = proxy;
        _log.LogInformation($"Proxy '{_proxySettings.ProxyAddress}' with user '{_proxySettings.Domain}\\{_proxySettings.User}'");
      }

      using (handler)
      using (var client = new HttpClient(handler))
      {
        client.BaseAddress = new Uri(baseAddress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				if(_wgApiConfiguration.HttpTimeoutInMinutes > 0)
				{
          client.Timeout = TimeSpan.FromMinutes(_wgApiConfiguration.HttpTimeoutInMinutes);
        }

        HttpResponseMessage response;
        if (isPostNeeded)
        {
          // Create httpContent
          var requestBody = GetBody(request);
          var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

          response = await client.PostAsync(request, httpContent);
        }
        else
        {
          _log.LogInformation($"Trying to Request '{baseAddress}{request}'. Client timeout is '{client.Timeout:c}'");
          var stopwatch = new Stopwatch();
          try
          {
            stopwatch.Start();
            response = await client.GetAsync(request);
            stopwatch.Stop();
            _log.LogInformation($"Elapsed '{stopwatch.Elapsed:c}'");
          }
          catch (Exception e)
          {
            stopwatch.Stop();
            _log.LogError(e, $"Elapsed '{stopwatch.Elapsed:c}'");

            throw;
          }
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

    public void DownloadFile(string uri, string fileName)
    {
      using (var webClient = new WebClient())
      {
        if (_proxySettings.UseProxy)
        {
          var proxy = new WebProxy(_proxySettings.ProxyAddress, true);
          proxy.Credentials = new NetworkCredential(
            _proxySettings.User,
            _proxySettings.PwdHash.DecryptString(Guid),
            _proxySettings.Domain);
          webClient.Proxy = proxy;
        }
        try
        {
          webClient.DownloadFile(new Uri(uri), fileName);
        }
        catch (WebException)
        {
          // do nothing
        }
      }
    }

    private string GetBody(string request)
    {
      return request.Substring(request.IndexOf('?') + 1);
    }
  }
}

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician.HealthCheck
{
  public class WargamingCheck : IHealthCheck
  {
    private readonly IWargamingApiClient _wargamingApiClient;

    public WargamingCheck(
      IWargamingApiClient wargamingApiClient)
    {
      _wargamingApiClient = wargamingApiClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
      try
      {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var dictionaries = await _wargamingApiClient.GetStaticDictionariesAsync();
        stopwatch.Stop();
        return HealthCheckResult.Healthy($"Got dictionaries from Wargaming for {stopwatch.ElapsedMilliseconds} ms");
      }
      catch (Exception e)
      {
        return HealthCheckResult.Unhealthy($"Could not read dictionaries from Wargaming: '{e.Message}'");
      }
    }
  }
}

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using WotBlitzStatician.Data.DataAccessors;

namespace WotBlitzStatician.HealthCheck
{
  public class DataBaseCheck : IHealthCheck
  {
    private readonly IBlitzStaticianDictionary _blitzStaticianDictionary;

    public DataBaseCheck(IBlitzStaticianDictionary blitzStaticianDictionary)
    {
      _blitzStaticianDictionary = blitzStaticianDictionary;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
      try
      {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var nations = await _blitzStaticianDictionary.GetAllNations();
        stopwatch.Stop();
        return HealthCheckResult.Healthy($"Got nations dictionary from DB for {stopwatch.ElapsedMilliseconds} ms");
      }
      catch (Exception e)
      {
        return HealthCheckResult.Unhealthy($"Could not read nations dictionary from DB: '{e.Message}'");
      }
    }
  }
}

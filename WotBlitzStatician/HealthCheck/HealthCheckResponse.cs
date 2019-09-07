using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WotBlitzStatician.HealthCheck
{
  public static class HealthCheckResponse
  {
    public static Task WriteResponse(HttpContext httpContext,
        HealthReport result)
    {
      httpContext.Response.ContentType = "application/json";

      var json = new JObject(
          new JProperty("status", result.Status.ToString()),
          new JProperty("results", new JObject(result.Entries.Select(pair =>
              new JProperty(pair.Key, new JObject(
                  new JProperty("status", pair.Value.Status.ToString()),
                  new JProperty("description", pair.Value.Description)
                  ))))));
      return httpContext.Response.WriteAsync(
          json.ToString(Formatting.Indented));
    }
  }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace WotBlitzStatician.NLogProvider
{
    public static class NLogExtensions
    {
		public static IWebHostBuilder UseNLog(this IWebHostBuilder builder, string logConfigName)
		{
			return builder.ConfigureLogging((logBuilder) => logBuilder.AddProvider(new NlogProvider(logConfigName)));
		}

	}
}

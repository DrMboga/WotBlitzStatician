using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;

namespace WotBlitzStatician.NLogProvider
{
    public class NlogProvider : ILoggerProvider
	{
		private readonly LogFactory logFactory;

		public NlogProvider(string configName)
		{
			logFactory = new LogFactory(
			new XmlLoggingConfiguration(configName)
			);
		}

		public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
		{
			return new NLogger(logFactory.GetLogger(categoryName));
		}

		public void Dispose()
		{
			logFactory.Dispose();
		}
	}
}

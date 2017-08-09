namespace WotBlitzStatician.Log4net
{
	using System.IO;
	using Microsoft.Extensions.Logging;

	public static class Log4NetExtensions
	{
		public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string log4NetConfigFile)
		{
			factory.AddProvider(new Log4NetProvider(log4NetConfigFile));
			return factory;
		}

		public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
		{
			string configPath = Path.Combine(Directory.GetCurrentDirectory(), "log4net.config");
			factory.AddProvider(new Log4NetProvider(configPath));
			return factory;
		}
	}
}
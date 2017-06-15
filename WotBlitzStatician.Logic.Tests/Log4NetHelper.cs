namespace WotBlitzStatician.Logic.Tests
{
	using System.IO;
	using System.Reflection;
	using System.Xml;
	using log4net;
	using log4net.Config;

	public class Log4NetHelper
	{
		public static void ConfigureLog4Net()
		{
			var log4NetConfig = new XmlDocument();
			log4NetConfig.Load(File.OpenRead("Log4net.xml"));

			var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
			XmlConfigurator.Configure(repo, log4NetConfig["log4net"]);
		}

	}
}
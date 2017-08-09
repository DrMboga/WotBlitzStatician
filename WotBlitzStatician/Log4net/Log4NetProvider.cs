﻿namespace WotBlitzStatician.Log4net
{
	using System.Collections.Concurrent;
	using System.IO;
	using System.Xml;
	using Microsoft.Extensions.Logging;

	public class Log4NetProvider : ILoggerProvider
	{
		private readonly string _log4NetConfigFile;
		private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers =
			new ConcurrentDictionary<string, Log4NetLogger>();

		public Log4NetProvider(string log4NetConfigFile)
		{
			_log4NetConfigFile = log4NetConfigFile;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
		}

		public void Dispose()
		{
			_loggers.Clear();
		}

		private Log4NetLogger CreateLoggerImplementation(string name)
		{
			return new Log4NetLogger(name, Parselog4NetConfigFile(_log4NetConfigFile));
		}

		private static XmlElement Parselog4NetConfigFile(string filename)
		{
			var log4NetConfig = new XmlDocument();
			log4NetConfig.Load(File.OpenRead(filename));
			return log4NetConfig["log4net"];
		}
	}
}
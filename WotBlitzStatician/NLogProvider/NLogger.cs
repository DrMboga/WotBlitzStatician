using System;
using Microsoft.Extensions.Logging;

namespace WotBlitzStatician.NLogProvider
{
    public class NLogger : ILogger
	{
		private readonly NLog.ILogger _logger;

		public NLogger(NLog.ILogger logger)
		{
			_logger = logger;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.Critical:
					return _logger.IsFatalEnabled;
				case LogLevel.Debug:
					return _logger.IsDebugEnabled;
				case LogLevel.Trace:
					return _logger.IsTraceEnabled;
				case LogLevel.Error:
					return _logger.IsErrorEnabled;
				case LogLevel.Information:
					return _logger.IsInfoEnabled;
				case LogLevel.Warning:
					return _logger.IsWarnEnabled;
				default:
					throw new ArgumentOutOfRangeException(nameof(logLevel));
			}
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			var message = state.ToString();
			if (exception != null)
			{
				message = $"{message}{Environment.NewLine}{exception}";
			}
			if (!string.IsNullOrEmpty(message) || exception != null)
			{
				switch (logLevel)
				{
					case LogLevel.Critical:
						_logger.Fatal(message);
						break;
					case LogLevel.Debug:
						_logger.Debug(message);
						break;
					case LogLevel.Trace:
						_logger.Trace(message);
						break;
					case LogLevel.Error:
						_logger.Error(message);
						break;
					case LogLevel.Information:
						_logger.Info(message);
						break;
					case LogLevel.Warning:
						_logger.Warn(message);
						break;
					default:
						_logger.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
						_logger.Info(exception, message);
						break;
				}
			}
		}
	}
}

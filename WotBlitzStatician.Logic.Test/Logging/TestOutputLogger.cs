using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WotBlitzStatician.Logic.Test.Logging
{
  public class TestOutputLogger : ILogger
  {
    private readonly ITestOutputHelper _output;

    public TestOutputLogger(ITestOutputHelper output)
    {
      _output = output;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      var message = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss.ffff}|{logLevel.ToString().ToUpper()}|{state}";
      if (exception != null)
      {
        message = $"{message}{Environment.NewLine}{exception}";
      }
      if (!string.IsNullOrEmpty(message) || exception != null)
      {
        _output.WriteLine(message);
      }
    }
  }
}
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WotBlitzStatician.Logic.Test.Logging
{
  public static class TestOutputLoggerFactory
  {
    public static ILoggerFactory CreateLoggerFactory(ITestOutputHelper output)
    {
      return new LoggerFactory(new List<ILoggerProvider> { new TestOutputLogProvider(output) });
    }
  }
}
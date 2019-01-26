using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WotBlitzStatician.Logic.Test.Logging
{
  public class TestOutputLogProvider : ILoggerProvider
  {
    private readonly ITestOutputHelper _output;

    public TestOutputLogProvider(ITestOutputHelper output)
    {
      _output = output;
    }

    public ILogger CreateLogger(string categoryName)
    {
      return new TestOutputLogger(_output);
    }

    public void Dispose()
    {
      
    }
  }
}
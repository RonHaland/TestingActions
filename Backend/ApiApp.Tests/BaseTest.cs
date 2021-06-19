using Serilog;

namespace ApiApp.Tests
{
    public class BaseTest
    {
        public ILogger Logger => new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();
    }
}

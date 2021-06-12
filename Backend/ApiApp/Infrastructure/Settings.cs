using System;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace ApiApp.Infrastructure
{
    public interface ISettings
    {
        public LogEventLevel LogLevel { get;  }
        public string TableConnectionString { get; }
    }


    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LogEventLevel LogLevel { get => _configuration.GetValue<LogEventLevel>("LogLevel"); }
        public string TableConnectionString { get => _configuration.GetValue<string>("TableConnectionString"); }

    }
}

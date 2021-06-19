using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace ApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hmm = CreateHostBuilder(args);
            var host = hmm.Build();
            var root = host.Services.GetAutofacRoot();
            using (var scope = root.BeginLifetimeScope())
            {
                var log = scope.Resolve<ILogger>();
                log.Information("Service started and running");
                try
                {
                    host.Run();
                }
                catch (Exception e)
                {
                    log.Fatal(e, "Fatal error");
                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureAppConfiguration((hostCtx, config) => 
                config
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

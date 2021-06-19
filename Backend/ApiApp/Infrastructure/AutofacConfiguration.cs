using ApiApp.Repository;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using System;

namespace ApiApp.Infrastructure
{
    public static class AutofacConfiguration
    {
        public static void Configure(ContainerBuilder builder, IConfiguration configuration)
        {

            var settings = new Settings(configuration);
            var test = Environment.GetEnvironmentVariables();
            var storage = CloudStorageAccount.Parse(settings.TableConnectionString);

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.Register(c => settings).As<ISettings>().InstancePerLifetimeScope();
            builder.Register(c => new LoggerConfiguration()
                                .MinimumLevel.Is(settings.LogLevel)
                                .WriteTo.Console()
                                .WriteTo.AzureTableStorage(storage)
                                .CreateLogger()).As<ILogger>().SingleInstance();
        }
    }
}

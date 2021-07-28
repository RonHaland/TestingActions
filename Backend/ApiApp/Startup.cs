using ApiApp.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace ApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder) 
        {
            AutofacConfiguration.Configure(builder, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseExceptionHandler(e => 
            {
                e.Run(async c =>
                {
                    c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    c.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = c.Features.Get<IExceptionHandlerPathFeature>();
                    using var scope = AutofacContainer.BeginLifetimeScope();
                    var log = scope.Resolve<ILogger>();
                    var ex = exceptionHandlerPathFeature?.Error;
                    log.Error(ex, "An error occured");
                    await c.Response.WriteAsync(JsonConvert.SerializeObject(ex));
                });
            });

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

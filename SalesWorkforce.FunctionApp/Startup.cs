using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SalesWorkforce.Common.Models;
using SalesWorkforce.FunctionApp.Providers;
using SalesWorkforce.FunctionApp.Providers.Abstractions;
using SalesWorkforce.FunctionApp.Services;
using SalesWorkforce.FunctionApp.Services.Abstractions;
using System.IO;

[assembly: FunctionsStartup(typeof(SalesWorkforce.FunctionApp.Startup))]

namespace SalesWorkforce.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig = null;

        private IConfigurationRoot FunctionConfig(string appDir) =>
            _functionConfig ??= new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(appDir, "appsettings.json"), optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;

            builder.Services.AddLogging();

            builder.Services.AddOptions<AppSettings>()
                .Configure<IOptions<ExecutionContextOptions>>((appSettings, exeContext) =>
                FunctionConfig(exeContext.Value.AppDirectory).GetSection("AppSettings").Bind(appSettings));

            builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSingleton<INotificationService, NotificationService>();
            builder.Services.AddSingleton<ISalesAgentService, SalesAgentService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IPurchaseOrderService, PurchaseOrderService>();
        }
    }
}

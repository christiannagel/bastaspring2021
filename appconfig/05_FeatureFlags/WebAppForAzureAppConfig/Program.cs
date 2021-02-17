using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using System;

namespace WebAppForAzureAppConfig
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults((webBuilder) =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        DefaultAzureCredential credential = new(includeInteractiveCredentials: true);
                        var settings = config.Build();

                        string azureAppConfigurationEndpoint = settings["AzureAppConfigurationEndpoint"];

                        config.AddAzureAppConfiguration(azureAppConfig =>
                        {
                            azureAppConfig.Connect(new Uri(azureAppConfigurationEndpoint), credential)
                            .Select(KeyFilter.Any, LabelFilter.Null)
                            .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                            .ConfigureRefresh(refresh =>
                            {
                                refresh.Register("AppConfigurationSolutionSample.Sentinel",
                                    refreshAll: true)
                                .SetCacheExpiration(TimeSpan.FromMinutes(5));
                            }).UseFeatureFlags();
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

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
                        // read from user secrets
                        string azureAppConfigurationEndpoint = settings["AzureAppConfigurationEndpoint"];

                        config.AddAzureAppConfiguration(azureAppConfig =>
                        {
                            azureAppConfig.Connect(new Uri(azureAppConfigurationEndpoint), credential)
                            .ConfigureRefresh(refresh =>
                            {
                                refresh.Register("AppConfigurationSolutionSample.MySettingsCategory.Sentinel",
                                    refreshAll: true)
                                .SetCacheExpiration(TimeSpan.FromMinutes(5));

                            })
                            .Select(KeyFilter.Any, LabelFilter.Null)
                            .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName);
                            

                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

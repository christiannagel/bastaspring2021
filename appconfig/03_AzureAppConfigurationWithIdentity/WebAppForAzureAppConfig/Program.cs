using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        DefaultAzureCredential credential = new();
                        var settings = config.Build();
                        string azureAppConfigurationEndpoint = settings["AzureAppConfigurationEndpoint"];

                        config.AddAzureAppConfiguration(azureAppConfig =>
                        {
                            azureAppConfig.Connect(new Uri(azureAppConfigurationEndpoint), credential);
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

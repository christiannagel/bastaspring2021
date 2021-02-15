using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
                        var settings = config.Build();
                        // read from user secrets
                        string azureAppConfigurationConnection = settings["AzureAppConfigurationConnection"];
                        config.AddAzureAppConfiguration(azureAppConfigurationConnection);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

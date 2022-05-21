using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotApiGateway
{
    public class Program
    {
        private const string LOGGING_CONFIG_SECTION = "Logging";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logginBuilder) =>
                {
                    var logginConfig = hostingContext.Configuration.GetSection(LOGGING_CONFIG_SECTION);
                    logginBuilder.AddConfiguration(logginConfig);
                    logginBuilder.AddConsole();
                    logginBuilder.AddDebug();
                });
    }
}

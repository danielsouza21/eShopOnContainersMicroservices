using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotApiGateway
{
    public class Program
    {
        private const string LOGGING_CONFIG_SECTION = "Logging";

        private const string OCELOT_CONFIG_FORMAT_JSON = "{0}{1}.json";
        private const string OCELOT_JSON_CONFIG_NAME = "ocelot";
        private const string DOT_STRING = ".";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = hostingContext.HostingEnvironment.EnvironmentName.Insert(0, DOT_STRING);
                    var ocelotFile = string.Format(OCELOT_CONFIG_FORMAT_JSON, OCELOT_JSON_CONFIG_NAME, environment);
                    config.AddJsonFile(ocelotFile, true, true);
                })
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

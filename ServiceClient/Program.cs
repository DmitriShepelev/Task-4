using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Task_4.BLL.Infrastructure;

namespace ServiceClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IHost CreateHostBuilder(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", true, true);
                    builder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
                })
                .UseWindowsService(options =>
                {
                    options.ServiceName = "Task4 Service";
                })
                .ConfigureLogging(logger =>
                    logger.AddEventLog(new EventLogSettings()
                    {
                        LogName = "Task4 Log",
                        SourceName = "Task4 Service",
                    }))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<AppOptions>(hostContext.Configuration.GetSection("AppOptions"));
                }).Build();
        }
    }
}

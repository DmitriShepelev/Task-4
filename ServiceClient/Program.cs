using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

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
            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "Task4 Service";
                })
                .ConfigureLogging(logger =>
                    logger.AddEventLog(new EventLogSettings()
                    {
                        LogName = "Task4 Log",
                        SourceName = "Task4Service",
                        Filter = (message, level) => true
                    }))
                .ConfigureServices((hostContext, services) => { services.AddHostedService<Worker>(); }).Build();
        }
    }
}

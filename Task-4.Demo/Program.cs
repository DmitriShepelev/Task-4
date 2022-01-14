using System.Configuration;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.AsyncHandlers;
using Task_4.BLL.DTOEntityParsers;
using Task_4.BLL.Handlers;
using Task_4.BLL.ProcessManagers;

namespace Task_4.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            //IConfiguration config = (new ConfigurationBuilder())
            //   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json").Build();
            

            //using (IAsyncApp app = new ConsoleClientApp(_config.GetSection("AppOptions").Get<AppOptions>()))
            //{
            //    Console.Write("Starting...");
            //    app.StartAsync().Wait();
            //    Console.WriteLine("listening");
            //    Console.ReadKey(true);
            //    app.StopAsync().Wait();
            //    Console.WriteLine("Stopped");
            //}
            var app = new StartApp();
            app.InitializeApp();
            app.StartAsync();

        }
    }
}

//using System.Configuration;
//using System;
//using Task_4.BLL.Infrastructure;

//namespace Task_4.Demo
//{
//    class Program
//    {
//static void Main(string[] args)
//{
//}
//        {
//            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
//            //IConfiguration config = (new ConfigurationBuilder())
//            //   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
//            //   .AddJsonFile("appsettings.json").Build();

//            //var connection = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionOptions"));

//            //using (IAsyncApp app = new ConsoleClientApp(_config.GetSection("AppOptions").Get<AppOptions>()))
//            //{
//            //    Console.Write("Starting...");
//            //    app.StartAsync().Wait();
//            //    Console.WriteLine("listening");
//            //    Console.ReadKey(true);
//            //    app.StopAsync().Wait();
//            //    Console.WriteLine("Stopped");
//            //}


//            var app = new StartApp();
//            var folderOptions = new AppOptions()
//            {
//                Source = ConfigurationManager.AppSettings.Get("Source"),
//                Target = ConfigurationManager.AppSettings.Get("Target"),
//                Pattern = ConfigurationManager.AppSettings.Get("Pattern")
//            };
//            app.InitializeApp();
//            app.StartAsync().Wait();

//            Console.ReadKey(true);

//            //folderManager.Run();

//        }
//    }
//}

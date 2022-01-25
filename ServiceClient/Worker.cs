using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.AsyncHandlers;
using Task_4.BLL.DataSources;
using Task_4.BLL.Factories;
using Task_4.BLL.Handlers;
using Task_4.BLL.Infrastructure;
using Task_4.BLL.ProcessManagers;
using Task_4.Contexts;

namespace ServiceClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IAsyncHandler<DataSourceDto> _app;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApp();
            var startAppTask = _app.StartAsync();
            _logger.LogInformation("Task4Service successfully running at: {time}", DateTimeOffset.Now);
            return startAppTask;
        }

        private void InitializeApp()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionOptions");
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Task4Context>());
            //IConnectionFactory connectionFactory = new Task4ConnectionFactory(ConfigurationManager.AppSettings.Get("ConnectionOptions"));
            //IContextFactory contextFactory = new Task4ContextFactory(connectionFactory);

            //IRepositoryFactory repoFactory = new Task4RepositoryFactory();

            var appOptions = new AppOptions()
            {
                Source = ConfigurationManager.AppSettings.Get("Source"),
                Target = ConfigurationManager.AppSettings.Get("Target"),
                Pattern = ConfigurationManager.AppSettings.Get("Pattern"),
                ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionOptions")
            };

            //IDataSourceFactory<DataSourceDto> dataSourceFactory = new DataSourceFactory(appOptions);

            //IDtoParserFactory<DataSourceDto> parserFactory = new DtoParserFactory();
            ConcurrencyLockProvider concurrencyLockProvider = new();

            IDataItemHandlerFactory<DataSourceDto> dataItemHandlerFactory =
                new DataItemHandlerFactory(concurrencyLockProvider, connectionString);
            
            //IDbConnectionHandler dbConnectionHandler =
            //    new DbConnectionHandler(connectionString);

            //IDataSourceHandlerFactory<DataSourceDto> dataSourceHandlerFactory = new DataSourceHandlerFactory(
            //    dbConnectionHandler, dataItemHandlerFactory);

            //var provider = new FolderDataSource(appOptions, dataSourceFactory);


            var folderManager =
                new FolderManager<DataSourceDto>(dataItemHandlerFactory, appOptions);


            var eventManager = new EventManager<DataSourceDto>(dataItemHandlerFactory, appOptions);
            //if (appOptions.Source != null && appOptions.Pattern != null)
            //    eventManager.Bind(new Watcher(new FileSystemWatcher(appOptions.Source, appOptions.Pattern)));


            //IAsyncHandler<DataSourceDto> folderAsyncHandler =
            //    new AsyncHandler<DataSourceDto>(folderManager, new ConcurrentBag<Task>());
            //IAsyncHandler<DataSourceDto> eventAsyncHandler =
            //    new AsyncHandler<DataSourceDto>(eventManager, new ConcurrentBag<Task>());
            //IAsyncHandlersCollection<DataSourceDto> handlersCollection = new AsyncHandlersCollection<DataSourceDto>();
            //handlersCollection.Add(eventAsyncHandler);
            //handlersCollection.Add(folderAsyncHandler);

            IAsyncHandler<DataSourceDto> asyncHandler = new AsyncHandler<DataSourceDto>();
            asyncHandler.Add(folderManager);
            asyncHandler.Add(eventManager);

            _app = asyncHandler;
        }
    }
}

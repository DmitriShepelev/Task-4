using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
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
using Task_4.DAL.Abstractions;
using Task_4.DAL.ConnectionFactories;
using Task_4.DAL.ContextFactories;
using Task_4.DAL.RepositoryFactories;
using System.Configuration;

namespace Task_4.Demo
{
    public class StartApp : IAsyncApp<DataSourceDto>
    {
        private IAsyncHandlersCollection<DataSourceDto> _app;
        public void InitializeApp()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Task4Context>());
            IConnectionFactory connectionFactory = new Task4ConnectionFactory("Task4Db");
            IContextFactory contextFactory = new Task4ContextFactory(connectionFactory);

            IRepositoryFactory repoFactory = new Task4RepositoryFactory();
            
            var folderOptions = new AppFolderOptions()
            {
                Source = ConfigurationManager.AppSettings.Get("Source"),
                Target = ConfigurationManager.AppSettings.Get("Target"),
                Pattern = ConfigurationManager.AppSettings.Get("Pattern")
            };
            IDataSourceFactory<DataSourceDto> dataSourceFactory = new DataSourceFactory(folderOptions);

            IDtoParserFactory<DataSourceDto> parserFactory = new DtoParserFactory();
            ConcurrencyLockProvider concurrencyLockProvider = new();

            IDataItemHandlerFactory<DataSourceDto> dataItemHandlerFactory =
                new DataItemHandlerFactory(repoFactory, contextFactory, concurrencyLockProvider, parserFactory);

            IDbConnectionHandler dbConnectionHandler =
                new DbConnectionHandler(contextFactory, repoFactory, connectionFactory);
            IDataSourceHandlerFactory<DataSourceDto> dataSourceHandlerFactory = new DataSourceHandlerFactory(
                dbConnectionHandler, dataItemHandlerFactory);

            var folderManager =
                new FolderManager<DataSourceDto>(dataSourceHandlerFactory, new FolderDataSource(folderOptions, dataSourceFactory));

            var eventManager = new EventManager<DataSourceDto>(dataSourceHandlerFactory, dataSourceFactory);


            IAsyncHandler<DataSourceDto> folderAsyncHandler =
                new AsyncHandler<DataSourceDto>(folderManager, new ConcurrentBag<Task>());
            IAsyncHandler<DataSourceDto> eventAsyncHandler =
                new AsyncHandler<DataSourceDto>(eventManager, new ConcurrentBag<Task>());
            IAsyncHandlersCollection<DataSourceDto> handlersCollection = new AsyncHandlersCollection<DataSourceDto>();
            handlersCollection.Add(folderAsyncHandler);
            handlersCollection.Add(eventAsyncHandler);
            _app = handlersCollection;
        }

        public Task StartAsync()
        {
           return _app.StartAsync();
        }
    }
}

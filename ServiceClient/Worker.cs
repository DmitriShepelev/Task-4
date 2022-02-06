using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Task_4.BLL.Abstractions;
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
        private readonly AppOptions _appOptions;

        public Worker(ILogger<Worker> logger, IOptions<AppOptions> appOptions)
        {
            _logger = logger;
            _appOptions = appOptions.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApp();
            _app.TaskFailed += (_, ds) => _logger.LogInformation($"Task on file {ds.FileName} failed");
            _app.TaskCompleted += (_, ds) => _logger.LogInformation($"Task on file {ds.FileName} completed");

            var startAppTask = _app.StartAsync();
            _logger.LogInformation("Task4Service successfully running at: {time}", DateTimeOffset.Now);
            return startAppTask;
        }

        private void InitializeApp()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
            
            ConcurrencyLockProvider concurrencyLockProvider = new();

            IDataItemHandlerFactory<DataSourceDto> dataItemHandlerFactory =
                new DataItemHandlerFactory(concurrencyLockProvider, _appOptions.ConnectionOptions);
            

            var folderManager = new FolderManager<DataSourceDto>(dataItemHandlerFactory, _appOptions);
            var eventManager = new EventManager<DataSourceDto>(dataItemHandlerFactory, _appOptions);

            IAsyncHandler<DataSourceDto> asyncHandler = new AsyncHandler<DataSourceDto>();
            asyncHandler.Add(folderManager);
            asyncHandler.Add(eventManager);

            _app = asyncHandler;
        }
    }
}

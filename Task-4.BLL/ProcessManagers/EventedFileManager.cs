using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;

namespace Task_4.BLL.ProcessManagers
{
    public class EventFileManager<DTOEntity> : BaseFileManager<DTOEntity>, IFileProcessManager<DTOEntity>
    {
        public EventFileManager(IDataSourceHandlerBuilder<DTOEntity> dataSourceHandlerBuilder,
            CancellationTokenSource tokenSource,
            TaskScheduler taskScheduler,
            FileSystemWatcher watcher) : base(dataSourceHandlerBuilder,
            tokenSource,
            taskScheduler)
        {
            Watcher = watcher;
        }

        protected FileSystemWatcher Watcher { get; set; }
       
        public Task Run()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            
        }
    }
}

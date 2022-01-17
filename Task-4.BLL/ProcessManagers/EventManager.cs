using System;
using System.IO;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public class EventManager<TDtoEntity> : BaseFileManager<TDtoEntity>, IProcessManager<TDtoEntity>, IRunnable
    {
        protected IDataSourceFactory<TDtoEntity> DataSourceFactory { get; set; }
        protected Watcher Watcher;
        public EventManager(IDataSourceHandlerFactory<TDtoEntity> dataSourceHandlerFactory,
            IDataSourceFactory<TDtoEntity> dataSourceFactory) : base(dataSourceHandlerFactory)
        {
            DataSourceFactory = dataSourceFactory;
        }

        public EventManager<TDtoEntity> Bind(Watcher watcher)
        {
            Watcher = watcher;
            Watcher.ActionConnector += Watcher_Created;
            return this;
        }
        
        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            if (Watcher is null) return;
            Watcher.Start();
            Watcher.WaitForStop();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            ProcessAction(DataSourceFactory.CreateInstance(e.FullPath));
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}

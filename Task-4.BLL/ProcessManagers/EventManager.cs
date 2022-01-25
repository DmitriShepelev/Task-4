using System;
using System.IO;
using System.Threading;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public class EventManager<TDtoEntity> : BaseFileManager<TDtoEntity>
    {
        private readonly AppOptions _appOptions;

        private Action<IFileDataSource<TDtoEntity>> _processAction;
       // protected IDataSourceFactory<TDtoEntity> DataSourceFactory { get; set; }
        //protected Watcher Watcher;
        private readonly FileSystemWatcher _watcher;
        private readonly ManualResetEvent _stopThreadEvent = new(false);
        public EventManager(IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory,
            //IDataSourceFactory<TDtoEntity> dataSourceFactory,
            AppOptions appOptions) : base(dataItemHandlerFactory)
        {
            _appOptions = appOptions;
            _watcher = new FileSystemWatcher(_appOptions.Source, _appOptions.Pattern);
            _watcher.Created += Watcher_Created;
            // DataSourceFactory = dataSourceFactory;
        }

        //public EventManager<TDtoEntity> Bind(Watcher watcher)
        //{
        //    Watcher = watcher;
        //    Watcher.ActionConnector += Watcher_Created;
        //    return this;
        //}
        
        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            //if (Watcher is null) return;
            //Watcher.Start();
            //Watcher.WaitForStop();
            _processAction = processAction;
            _watcher.EnableRaisingEvents = true;
            _stopThreadEvent.WaitOne();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            _processAction((IFileDataSource<TDtoEntity>) new FileDataSource(e.FullPath, _appOptions.Target));
            _stopThreadEvent.Reset();
        }

        //public void Run()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

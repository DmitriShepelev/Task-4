using System;
using System.IO;
using System.Threading;
using Task_4.BLL.Abstractions;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public class EventManager<TDtoEntity> : BaseFileManager<TDtoEntity>
    {
        private readonly AppOptions _appOptions;

        private Action<IFileDataSource<TDtoEntity>> _processAction;
        private readonly FileSystemWatcher _watcher;
        private readonly ManualResetEvent _stopThreadEvent = new(false);
        public EventManager(IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory, AppOptions appOptions) : base(dataItemHandlerFactory)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            _appOptions = appOptions;
            _watcher = new FileSystemWatcher(_appOptions.Source, _appOptions.Pattern);
            //_watcher = watcher;
            _watcher.Created += Watcher_Created;
        }
        
        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            _processAction = processAction;
            _watcher.EnableRaisingEvents = true;
            _stopThreadEvent.WaitOne();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            _processAction((IFileDataSource<TDtoEntity>) new FileDataSource(e.FullPath, _appOptions.Target));
            _stopThreadEvent.Reset();
        }
    }
}

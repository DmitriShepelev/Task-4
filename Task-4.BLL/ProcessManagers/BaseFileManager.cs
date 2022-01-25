using System;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.ProcessManagers
{
    public abstract class BaseFileManager<TDtoEntity> : IProcessManager<TDtoEntity>
    {
        // protected CancellationTokenSource CancellationTokenSource { get; set; }
        // protected TaskScheduler TaskScheduler { get; set; }
        //protected IDataSourceHandlerFactory<TDtoEntity> DataSourceHandlerFactory { get; set; }
        private readonly IDataItemHandlerFactory<TDtoEntity> _dataItemHandlerFactory;

        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted;
        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed;
        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskInterrupted;

        protected BaseFileManager(
            //IDataSourceHandlerFactory<TDtoEntity>dataSourceHandlerFactory,
            IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory) // CancellationTokenSource tokenSource, TaskScheduler taskScheduler)
        {
            //DataSourceHandlerFactory = dataSourceHandlerFactory;
            _dataItemHandlerFactory = dataItemHandlerFactory;
            //CancellationTokenSource = tokenSource;
            //TaskScheduler = taskScheduler;
        }


        public abstract void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction);

        public virtual void ProcessAction(IFileDataSource<TDtoEntity> source)
        {
            //return Task.Factory.StartNew<TaskCompletionStatus>(() =>
            //{

            try
            {
                using var handler = new DataSourceHandler<TDtoEntity>(
                    source,
                    _dataItemHandlerFactory.CreateInstance()
                    //new DbConnectionHandler(_connectionString)
                    );
                handler.Run();
                // return TaskCompletionStatus.Success;
            }
            catch (HandlerException)
            {
                //return TaskCompletionStatus.Failed;
            }
            catch (OperationCanceledException)
            {
                // return TaskCompletionStatus.Interrupted;
            }
            catch (Exception)
            {
                //return TaskCompletionStatus.Failed;
            }
        }
        //        }, CancellationTokenSource.Token, TaskCreationOptions.None, TaskScheduler)
        //        .ContinueWith(t =>
        //        {
        //            switch (t.Result)
        //            {
        //                case TaskCompletionStatus.Success:
        //                    OnTaskCompleted(this, source);
        //                    break;
        //                case TaskCompletionStatus.Failed:
        //                    OnTaskFailed(this, source);
        //                    break;
        //                case TaskCompletionStatus.Interrupted:
        //                    OnTaskInterrupted(this, source);
        //                    break;
        //            }
        //        });
        //}
        //protected virtual void OnTaskCompleted(object sender, IFileDataSource<TDtoEntity> source)
        //{
        //    EventHandler<IFileDataSource<TDtoEntity>> temp = null;
        //    Interlocked.Exchange(ref temp, TaskCompleted);
        //    temp?.Invoke(sender, source);
        //}

        //protected virtual void OnTaskFailed(object sender, IFileDataSource<TDtoEntity> source)
        //{
        //    EventHandler<IFileDataSource<TDtoEntity>> temp = null;
        //    Interlocked.Exchange(ref temp, TaskFailed);
        //    temp?.Invoke(sender, source);
        //}

        //protected virtual void OnTaskInterrupted(object sender, IFileDataSource<TDtoEntity> source)
        //{
        //    EventHandler<IFileDataSource<TDtoEntity>> temp = null;
        //    Interlocked.Exchange(ref temp, TaskInterrupted);
        //    temp?.Invoke(sender, source);
        //}
    }
}
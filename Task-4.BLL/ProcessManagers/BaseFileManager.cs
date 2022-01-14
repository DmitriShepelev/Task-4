using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.ProcessManagers
{
    public abstract class BaseFileManager<TDtoEntity>
    {
        // protected CancellationTokenSource CancellationTokenSource { get; set; }
        // protected TaskScheduler TaskScheduler { get; set; }
        protected IDataSourceHandlerFactory<TDtoEntity> DataSourceHandlerFactory { get; set; }

        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted;
        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed;
        //public event EventHandler<IFileDataSource<TDtoEntity>> TaskInterrupted;

        protected BaseFileManager(
            IDataSourceHandlerFactory<TDtoEntity>
                    dataSourceHandlerFactory) // CancellationTokenSource tokenSource, TaskScheduler taskScheduler)
        {
            DataSourceHandlerFactory = dataSourceHandlerFactory;
            //CancellationTokenSource = tokenSource;
            //TaskScheduler = taskScheduler;
        }


        public abstract void StartProcess(Action<IFileDataSource<TDtoEntity>> pendingTask);

        public virtual void PendingTask(IFileDataSource<TDtoEntity> source)
        {
            //return Task.Factory.StartNew<TaskCompletionStatus>(() =>
            //{

            try
            {
                using var handler = DataSourceHandlerFactory.CreateInstance(source);
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
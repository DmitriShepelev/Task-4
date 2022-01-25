using System;
using System.Threading;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Handlers;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public abstract class BaseFileManager<TDtoEntity> : IProcessManager<TDtoEntity>
    {
        private readonly IDataItemHandlerFactory<TDtoEntity> _dataItemHandlerFactory;

        public event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted;
        public event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed;

        protected BaseFileManager(IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory)
        {
            _dataItemHandlerFactory = dataItemHandlerFactory;
        }

        protected virtual void OnTaskCompleted(object sender, IFileDataSource<TDtoEntity> source)
        {
            EventHandler<IFileDataSource<TDtoEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskCompleted);
            temp?.Invoke(sender, source);
        }
        protected virtual void OnTaskFailed(object sender, IFileDataSource<TDtoEntity> source)
        {
            EventHandler<IFileDataSource<TDtoEntity>> temp = null;
            Interlocked.Exchange(ref temp, TaskFailed);
            temp?.Invoke(sender, source);
        }

        public abstract void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction);

        public virtual void ProcessAction(IFileDataSource<TDtoEntity> source)
        {
            var status = TaskCompletionStatus.Success;
            try
            {
                using var handler = new DataSourceHandler<TDtoEntity>(source, _dataItemHandlerFactory.CreateInstance());

                handler.Run();
            }
            catch (HandlerException)
            {
                status = TaskCompletionStatus.Failed;
            }
            catch (Exception)
            {
                status = TaskCompletionStatus.Failed;
            }
            Callback(status, source);
        }

        private protected virtual void Callback(TaskCompletionStatus status, IFileDataSource<TDtoEntity> source)
        {
            switch (status)
            {
                case TaskCompletionStatus.Success:
                    OnTaskCompleted(this, source);
                    break;
                case TaskCompletionStatus.Failed:
                    OnTaskFailed(this, source);
                    break;
                default:
                    throw new HandlerException(new ArgumentOutOfRangeException(nameof(status), status, null));
            }
        }
    }
}
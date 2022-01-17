using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.AsyncHandlers
{
    public class AsyncHandler<TDtoEntity> : IAsyncHandler<TDtoEntity>, ITaskEventable<TDtoEntity>
    {
        private readonly IProcessManager<TDtoEntity> _manager;
        private Task _mainProcessTask; 
        protected IProducerConsumerCollection<Task> TaskCollection;

        public AsyncHandler(IProcessManager<TDtoEntity> manager, IProducerConsumerCollection<Task> taskCollection)
        {
            _manager = manager;
            TaskCollection = taskCollection;
        }
        

        public void PendingTask(IFileDataSource<TDtoEntity> source)
        {
            var temp = Task.Factory.StartNew(
                () => _manager.ProcessAction(source),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);

            if (!TaskCollection.TryAdd(temp))
            {
                throw new InvalidOperationException("cannot pending task");
            }
        }
        public Task WhenAll()
        {
            return Task.WhenAll(TaskCollection);
        }

        public Task StartMainProcess()
        {
            try
            {
                return Task.Factory.StartNew(
                        () => Interlocked.Exchange(
                            ref _mainProcessTask,
                            Task.Factory.StartNew(() => _manager.StartProcess(PendingTask))))
                    .ContinueWith(t => Interlocked.Exchange(ref _mainProcessTask, null));
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
        }

        public Task WhenMainProcess()
        {
            var temp = _mainProcessTask;
            Interlocked.Exchange(ref temp, _mainProcessTask);
            return temp ?? Task.CompletedTask;
        }
    }
}

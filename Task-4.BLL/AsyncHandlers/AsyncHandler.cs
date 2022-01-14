using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.AsyncHandlers
{
    public class AsyncHandler<TDtoEntity> : IAsyncHandler<TDtoEntity>
    {
        private readonly IProcessManager<TDtoEntity> _handler;
        private Task _mainProcessTask; 
        private readonly IProducerConsumerCollection<Task> _taskCollection;

        public AsyncHandler(IProcessManager<TDtoEntity> handler, IProducerConsumerCollection<Task> taskCollection)
        {
            _handler = handler;
            _taskCollection = taskCollection;
        }
        

        public void PendingTask(IFileDataSource<TDtoEntity> source)
        {
            var temp = Task.Factory.StartNew(
                () => _handler.PendingTask(source),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);

            if (!_taskCollection.TryAdd(temp))
            {
                throw new InvalidOperationException("cannot pending task");
            }
        }
        public Task WhenAll()
        {
            return Task.WhenAll(_taskCollection);
        }

        public Task StartMainProcess()
        {
            try
            {
                return Task.Factory.StartNew(
                        () => Interlocked.Exchange(
                            ref _mainProcessTask,
                            Task.Factory.StartNew(() => _handler.StartProcess(PendingTask))))
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

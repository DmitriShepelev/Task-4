using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.Handlers
{
    public class ParallelismHandler : IParallelismHandler
    {
        public CancellationTokenSource CancellationTokenSource { get; protected set; }
        public CancellationTokenSource StopTokenSource { get; protected set; }
       // public TaskScheduler TaskScheduler { get; protected set; }
        protected IProducerConsumerCollection<Task> TaskCollection { get; set; }

        public object SyncObj { get; } = new();

        public ParallelismHandler(CancellationTokenSource cancellationTokenSource,
            CancellationTokenSource stopTokenSource,
            //TaskScheduler taskScheduler,
            IProducerConsumerCollection<Task> tasks)
        {
            CancellationTokenSource = cancellationTokenSource;
            StopTokenSource = stopTokenSource;
           // TaskScheduler = taskScheduler;
            TaskCollection = tasks;
        }

        public void Add(Task task)
        {
            if (!TaskCollection.TryAdd(task))
            {
                throw new InvalidOperationException("Cannot handle task");
            }
        }

        public void Remove(Task task)
        {
            Task tempTask = task;
            if (!TaskCollection.TryTake(out tempTask))
            {
                throw new InvalidOperationException("Cannot handle task");
            }
        }

        public async Task RequestCancel()
        {
            StopTokenSource.Cancel();
            CancellationTokenSource.Cancel();
            await WaitForCompletion();
        }

        public async Task RequestStop()
        {
            StopTokenSource.Cancel();
            await WaitForCompletion();
        }

        public async Task WaitForCompletion()
        {
            await Task.WhenAll(TaskCollection);
        }

        public bool TryGetLock()
        {
            return Monitor.TryEnter(SyncObj, 0);
        }

        public void ExitLock()
        {
            Monitor.Exit(SyncObj);
        }
    }
}

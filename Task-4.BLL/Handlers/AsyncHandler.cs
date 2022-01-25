using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.Handlers
{
    public class AsyncHandler<TDtoEntity> : IAsyncHandler<TDtoEntity>
    {
        protected BlockingCollection<Task> TaskCollection;
        protected ICollection<IProcessManager<TDtoEntity>> Managers { get; set; }

        public AsyncHandler()
        {
            Managers = new List<IProcessManager<TDtoEntity>>();
            TaskCollection = new BlockingCollection<Task>();
        }

        public virtual event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted
        {
            add { foreach (var i in Managers) { i.TaskCompleted += value; } }
            remove { foreach (var i in Managers) { i.TaskCompleted -= value; } }
        }
        public virtual event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed
        {
            add { foreach (var i in Managers) { i.TaskFailed += value; } }
            remove { foreach (var i in Managers) { i.TaskFailed -= value; } }
        }

        public IAsyncHandler<TDtoEntity> Add(IProcessManager<TDtoEntity> manager)
        {
            Managers.Add(manager);
            return this;
        }


        public Task StartAsync()
        {

            foreach (var manager in Managers)
            {
                void PendingTask(IFileDataSource<TDtoEntity> x) => Task.Factory.StartNew(
                    () => manager.ProcessAction(x),
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskScheduler.Default
                    );

                var temp = Task.Factory.StartNew(() => { manager.StartProcess(PendingTask); });
                if (!TaskCollection.TryAdd(temp))
                    throw new InvalidOperationException("cannot pending task");
            }

            return Task.WhenAll(TaskCollection);
        }


    }
}

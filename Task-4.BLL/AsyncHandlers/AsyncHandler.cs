using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.AsyncHandlers
{
    public class AsyncHandler<TDtoEntity> : IAsyncHandler<TDtoEntity>//, ITaskEventable<TDtoEntity>
    {
        //private readonly IProcessManager<TDtoEntity> _manager;
        //private Task _mainProcessTask; 
        protected BlockingCollection<Task> TaskCollection;
        protected ICollection<IProcessManager<TDtoEntity>> Managers { get; set; }

        public AsyncHandler(
            //IProcessManager<TDtoEntity> manager, IProducerConsumerCollection<Task> taskCollection
            )
        {
            //_manager = manager;
            Managers = new List<IProcessManager<TDtoEntity>>();
            TaskCollection = new BlockingCollection<Task>();
        }

        public IAsyncHandler<TDtoEntity> Add(IProcessManager<TDtoEntity> manager)
        {
            Managers.Add(manager);
            return this;
        }

        //public void PendingTask(IFileDataSource<TDtoEntity> source)
        //{
        //    //var temp = 
        //    Task.Factory.StartNew(
        //    () => _manager.ProcessAction(source),
        //    CancellationToken.None,
        //    TaskCreationOptions.None,
        //    TaskScheduler.Default);

        //    //if (!TaskCollection.TryAdd(temp))
        //    //{
        //    //    throw new InvalidOperationException("cannot pending task");
        //    //}
        //}

        //public Task WhenAll()
        //{
        //    return Task.WhenAll(TaskCollection);
        //}

        //public Task StartMainProcess()
        //{
        //    try
        //    {
        //        return Task.Factory.StartNew(
        //                        //() => Interlocked.Exchange(
        //                        //    ref _mainProcessTask,
        //                        //    Task.Factory.StartNew(
        //                        () => _manager.StartProcess(PendingTask))//))
        //                                                                 //.ContinueWith(t => Interlocked.Exchange(ref _mainProcessTask, null))
        //            ;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new HandlerException(e);
        //    }
        //}

        public Task StartAsync()
        {

            foreach (var manager in Managers)
            {
                void PendingTask(IFileDataSource<TDtoEntity> x) => Task.Factory.StartNew(
                    () => manager.ProcessAction(x),
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskScheduler.Default);

                var temp = Task.Factory.StartNew(() => { manager.StartProcess(PendingTask); });
                if (!TaskCollection.TryAdd(temp))
                    throw new InvalidOperationException("cannot pending task");
            }

            return Task.WhenAll(TaskCollection);
        }
        //public Task WhenMainProcess()
        //{
        //    var temp = _mainProcessTask;
        //    Interlocked.Exchange(ref temp, _mainProcessTask);
        //    return temp ?? Task.CompletedTask;
        //}
    }
}

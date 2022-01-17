using System;
using System.Collections.Generic;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;

namespace Task_4.BLL.ProcessManagers
{
    public class FolderManager<TDtoEntity> : BaseFileManager<TDtoEntity>, IProcessManager<TDtoEntity>, IRunnable
    {
        //public  ParallelismHandler ParallelismHandler { get; }
        protected IEnumerable<IFileDataSource<TDtoEntity>> Provider { get; set; }

        public FolderManager(IDataSourceHandlerFactory<TDtoEntity> dataSourceHandlerFactory,
            IEnumerable<IFileDataSource<TDtoEntity>> provider) : base(dataSourceHandlerFactory)
        {
            Provider = provider;
        }

        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            foreach (var c in Provider)
            {
                //if (Tokens.Cancel.IsCancellationRequested || Tokens.Cancel.IsCancellationRequested)
                //{
                //    break;
                //}
                processAction(c);
            }
        }

        public virtual void Run()
        {
            StartProcess(ProcessAction);
        }

        //ICollection<Task> tasksList = Provider.TakeWhile(c => !CancellationTokenSource.IsCancellationRequested)
        //    .Select(CreateTask)
        //    .ToList();

        //if (tasksList.Count > 0)
        //{
        //    await Task.WhenAll(tasksList.ToArray());
        //}
        //    try
        //    {
        //        var syncResult = ParallelismHandler.TryGetLock();
        //      if (!syncResult) return null;

        //        var temp = Task.Factory.StartNew(() =>
        //        {
        //            foreach (var c in Provider)
        //            {
        //                if (ParallelismHandler.StopTokenSource.IsCancellationRequested)
        //                {
        //                    break;
        //                }

        //                ParallelismHandler.Add(CreateTask(c));
        //            }
        //        });

        //        return temp;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new HandlerException(e);
        //    }
        //    finally
        //    {
        //       ParallelismHandler.ExitLock();
        //    }
        //}

    }
}

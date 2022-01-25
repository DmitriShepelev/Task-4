using System;
using System.Collections.Generic;
using System.IO;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public class FolderManager<TDtoEntity> : BaseFileManager<TDtoEntity>
    {
        //public  ParallelismHandler ParallelismHandler { get; }
        private readonly AppOptions _appOptions;
        //protected IEnumerable<IFileDataSource<TDtoEntity>> Provider { get; set; }
        public FolderManager(IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory,
            //IEnumerable<IFileDataSource<TDtoEntity>> provider,
            AppOptions appOptions) : base(dataItemHandlerFactory)
        {
            //Provider = provider;
            _appOptions = appOptions;
        }

        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            foreach (var fileDataSource in GetFiles())
            {
                //if (Tokens.Cancel.IsCancellationRequested || Tokens.Cancel.IsCancellationRequested)
                //{
                //    break;
                //}
                processAction(fileDataSource);
            }
        }

        //public virtual void Run()
        //{
        //    StartProcess(ProcessAction);
        //}

        private IEnumerable<IFileDataSource<TDtoEntity>> GetFiles()
        {
            var files = Directory.GetFiles(_appOptions.Source, _appOptions.Pattern);
            foreach (var fileName in files)
            {
                yield return (IFileDataSource<TDtoEntity>)new FileDataSource(fileName, _appOptions.Target);
            }
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

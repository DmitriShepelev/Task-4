using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.DTOEntityParsers;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.ProcessManagers
{
    public class FolderManager<DTOEntity> : BaseFileManager<DTOEntity>, IFileProcessManager<DTOEntity>
    {
        public  ParallelismHandler ParallelismHandler { get; }
        protected IEnumerable<IFileDataSource<DTOEntity>> Provider { get; set; }

        public FolderManager(IDataSourceHandlerBuilder<DTOEntity> dataSourceHandlerBuilder,
            CancellationTokenSource tokenSource,
            IEnumerable<IFileDataSource<DTOEntity>> provider,
            TaskScheduler taskScheduler, ParallelismHandler handler) : base(dataSourceHandlerBuilder,
            tokenSource,
            taskScheduler)
        {
            Provider = provider;
            ParallelismHandler = handler;
        }

        public virtual  Task Run()
        {
            //ICollection<Task> tasksList = Provider.TakeWhile(c => !CancellationTokenSource.IsCancellationRequested)
            //    .Select(CreateTask)
            //    .ToList();

            //if (tasksList.Count > 0)
            //{
            //    await Task.WhenAll(tasksList.ToArray());
            //}
            try
            {
                var syncResult = ParallelismHandler.TryGetLock();
              if (!syncResult) return null;

                var temp = Task.Factory.StartNew(() =>
                {
                    foreach (var c in Provider)
                    {
                        if (ParallelismHandler.StopTokenSource.IsCancellationRequested)
                        {
                            break;
                        }

                        ParallelismHandler.Add(CreateTask(c));
                    }
                });

                return temp;
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
            finally
            {
               ParallelismHandler.ExitLock();
            }
        }
    }
}

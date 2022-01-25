using System;

namespace Task_4.BLL.Abstractions
{
   public interface IProcessManager<TDtoEntity> 
    {
       void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction);
       void ProcessAction(IFileDataSource<TDtoEntity> source);

        event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted;
        event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed;
    }
}

using System;

namespace Task_4.BLL.Abstractions
{
   public interface IProcessManager<TDtoEntity> : ITaskEventable<TDtoEntity>
    {
       void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction);
       void ProcessAction(IFileDataSource<TDtoEntity> source);
        
       //event EventHandler<IFileDataSource<DTOEntity>> TaskCompleted;
       //event EventHandler<IFileDataSource<DTOEntity>> TaskFailed;
       //event EventHandler<IFileDataSource<DTOEntity>> TaskInterrupted;
   }
}

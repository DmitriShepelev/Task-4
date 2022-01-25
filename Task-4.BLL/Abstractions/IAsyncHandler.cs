using System;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IAsyncHandler<TDtoEntity>
    {
        public IAsyncHandler<TDtoEntity> Add(IProcessManager<TDtoEntity> manager);
        public Task StartAsync();

        event EventHandler<IFileDataSource<TDtoEntity>> TaskCompleted;
        event EventHandler<IFileDataSource<TDtoEntity>> TaskFailed;
    }
}

using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IAsyncHandler<TDtoEntity>
    {
        public IAsyncHandler<TDtoEntity> Add(IProcessManager<TDtoEntity> manager);

        public Task StartAsync();
        //void PendingTask(IFileDataSource<TDtoEntity> source);
        //Task WhenAll();
        //Task StartMainProcess();
        //Task WhenMainProcess();
    }
}

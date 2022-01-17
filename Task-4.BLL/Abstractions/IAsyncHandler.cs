using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IAsyncHandler<TDtoEntity> 
    {
        void PendingTask(IFileDataSource<TDtoEntity> source);
        Task WhenAll();
        Task StartMainProcess();
        Task WhenMainProcess();
    }
}

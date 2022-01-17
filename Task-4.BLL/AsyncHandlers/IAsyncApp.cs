using System.Threading.Tasks;

namespace Task_4.BLL.AsyncHandlers
{
   public interface IAsyncApp<T>
    {
        Task StartAsync();
    }
}

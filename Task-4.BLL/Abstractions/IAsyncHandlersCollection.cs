using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
   public interface IAsyncHandlersCollection<T>
   {
       IAsyncHandlersCollection<T> Add(IAsyncHandler<T> handler);
       Task StartAsync();
    }
}

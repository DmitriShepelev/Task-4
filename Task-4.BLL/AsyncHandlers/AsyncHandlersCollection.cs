using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.AsyncHandlers
{
    public class AsyncHandlersCollection<TDtoEntity> : IAsyncHandlersCollection<TDtoEntity>, ITaskEventable<TDtoEntity>
    {
        protected ICollection<IAsyncHandler<TDtoEntity>> AsyncHandlers { get; set; }

        public AsyncHandlersCollection()
        {
            AsyncHandlers = new List<IAsyncHandler<TDtoEntity>>();
        }

        public IAsyncHandlersCollection<TDtoEntity> Add(IAsyncHandler<TDtoEntity> handler)
        {
            AsyncHandlers.Add(handler);
            return this;
        }

        public Task StartAsync()
        {
            return Task.WhenAll(AsyncHandlers.Select(x => x.StartMainProcess()));
        }
    }
}

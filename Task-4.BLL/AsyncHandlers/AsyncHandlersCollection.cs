using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.AsyncHandlers
{
    public class AsyncHandlersCollection<TDtoEntity> : IAsyncHandlersCollection<TDtoEntity>
    {
        private readonly ICollection<IAsyncHandler<TDtoEntity>> _asyncHandlers;

        public AsyncHandlersCollection()
        {
            _asyncHandlers = new List<IAsyncHandler<TDtoEntity>>();
        }

        public IAsyncHandlersCollection<TDtoEntity> Add(IAsyncHandler<TDtoEntity> handler)
        {
            _asyncHandlers.Add(handler);
            return this;
        }

        public Task StartAsync()
        {
            return Task.WhenAll(_asyncHandlers.Select(x => x.StartMainProcess()));
        }
    }
}

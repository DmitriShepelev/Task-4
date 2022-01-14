using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
   public interface IAsyncHandlersCollection<T>
   {
       IAsyncHandlersCollection<T> Add(IAsyncHandler<T> handler);
       Task StartAsync();
    }
}

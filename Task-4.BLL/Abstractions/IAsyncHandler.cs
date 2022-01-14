using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IAsyncHandler<DTOEntity> 
    {
        Task WhenAll();
        Task StartMainProcess();
        Task WhenMainProcess();
    }
}

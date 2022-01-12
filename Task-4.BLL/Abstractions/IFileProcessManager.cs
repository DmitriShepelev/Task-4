using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
   public interface IFileProcessManager<DTOEntity>
   {
       Task Run();
       event EventHandler<IFileDataSource<DTOEntity>> TaskCompleted;
       event EventHandler<IFileDataSource<DTOEntity>> TaskFailed;
       event EventHandler<IFileDataSource<DTOEntity>> TaskInterrupted;
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
   public interface IDbConnectionHandler
    {
        void Commit(bool sessionCompletedState);
        void Rollback(bool sessionCompletedState);
    }
}

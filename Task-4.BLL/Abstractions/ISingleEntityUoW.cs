using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Abstractions;

namespace Task_4.BLL.Abstractions
{
   public interface ISingleEntityUoW<TEntity> : IDisposable where TEntity : class
    {
        IGenericRepository<TEntity> Repository { get; }
    }
}

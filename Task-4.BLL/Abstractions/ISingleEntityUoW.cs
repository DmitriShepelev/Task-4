using System;
using Task_4.DAL.Abstractions;

namespace Task_4.BLL.Abstractions
{
   public interface ISingleEntityUoW<TEntity> : IDisposable where TEntity : class
    {
        IGenericRepository<TEntity> Repository { get; }
    }
}

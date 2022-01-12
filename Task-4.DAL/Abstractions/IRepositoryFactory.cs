using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Task_4.DAL.Abstractions
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class;
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Abstractions;
using Task_4.DAL.Repositories;

namespace Task_4.DAL.RepositoryFactories
{
  public  class Task4RepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class
        {
            return new Task4GenericRepository<TEntity>(context);
        }
    }
}

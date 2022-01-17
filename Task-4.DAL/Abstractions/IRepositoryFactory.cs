using System.Data.Entity;
namespace Task_4.DAL.Abstractions
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class;
    }
}

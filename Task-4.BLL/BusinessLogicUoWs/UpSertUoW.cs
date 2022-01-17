using System;
using System.Linq.Expressions;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Infrastructure;
using Task_4.DAL.Abstractions;

namespace Task_4.BLL.BusinessLogicUoWs
{
    public class UpsertUoW<TEntity> : BaseUoW<TEntity>, IUpsertUoW<TEntity> where TEntity : class
    {
        private readonly ConcurrencyLockProvider _concurrencyLock;
        public UpsertUoW(IGenericRepository<TEntity> repository, ConcurrencyLockProvider concurrencyLock) : base(repository)
        {
            _concurrencyLock = concurrencyLock;
        }

        public TEntity TakeAction(Expression<Func<TEntity, bool>> predicate, TEntity entityForInsert)
        {
            var locker = _concurrencyLock.GetLock<TEntity>();
            try
            {
                locker.EnterUpgradeableReadLock();
                var item = Repository.First(predicate);
                if (item == null)
                {
                    try
                    {
                        locker.EnterWriteLock();
                        Repository.Add(entityForInsert);
                        Repository.Context.SaveChanges();
                        return entityForInsert;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }

                }
                else return item;

            }
            finally
            {
                locker.ExitUpgradeableReadLock();
            }
        }
    }
}

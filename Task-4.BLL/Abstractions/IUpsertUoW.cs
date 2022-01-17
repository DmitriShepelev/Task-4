using System;
using System.Linq.Expressions;

namespace Task_4.BLL.Abstractions
{
    public interface IUpsertUoW<TEntity> : ISingleEntityUoW<TEntity> where TEntity : class
    {
        TEntity TakeAction(Expression<Func<TEntity, bool>> predicate, TEntity entityForInsert);
    }
}

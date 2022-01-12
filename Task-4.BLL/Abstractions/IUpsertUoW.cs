using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IUpsertUoW<TEntity> : ISingleEntityUoW<TEntity> where TEntity : class
    {
        TEntity TakeAction(Expression<Func<TEntity, bool>> predicate, TEntity entityForInsert);
    }
}

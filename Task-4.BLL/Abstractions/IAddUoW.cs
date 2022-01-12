using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IAddUoW<TEntity> : ISingleEntityUoW<TEntity> where TEntity : class
    {
        void TakeAction(TEntity entityItem);
    }
}

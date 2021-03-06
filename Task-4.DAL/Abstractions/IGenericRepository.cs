using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Task_4.DAL.Abstractions
{
    public interface IGenericRepository<T> : IDisposable where  T : class
    {
        DbContext Context { get; }
        T First(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null);
        T Get(int id);
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
        void Update(T item);
    }
}

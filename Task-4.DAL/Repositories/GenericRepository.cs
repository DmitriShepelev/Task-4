using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Abstractions;

namespace Task_4.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext Context;
        private bool _disposedValue;

        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        DbContext IGenericRepository<T>.Context => Context;

        public void Add(T item)
        {
            Context.Set<T>().Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            Context.Set<T>().AddRange(items);
        }
        
        public T First(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? Context.Set<T>().FirstOrDefault(predicate) : Context.Set<T>().FirstOrDefault();
        }
         
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? Context.Set<T>().Where(predicate) : Context.Set<T>();
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Remove(T item)
        {
            Context.Set<T>().Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            Context.Set<T>().RemoveRange(items);
        }

        public void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                Context.Dispose();
                Context = null;
            }

            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

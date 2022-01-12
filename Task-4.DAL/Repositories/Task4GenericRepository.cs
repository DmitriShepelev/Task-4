using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Abstractions;

namespace Task_4.DAL.Repositories
{
    public class Task4GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // переделать реализацию IDisposable
        protected DbContext Context;
        private bool _disposedValue;

        public Task4GenericRepository(DbContext dbContext)
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
            return predicate != null ? Context.Set<T>().FirstOrDefault(predicate) : Context.Set<T>().First();
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
            //Context.Entry(item).State = EntityState.Modified;
            Context.Set<T>().Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            Context.Set<T>().RemoveRange(items);
        }

        public void Update(T item)
        {
            //var entry = Context.Entry<T>(item);
            Context.Entry(item).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Context.Dispose();
                Context = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Task4GenericRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

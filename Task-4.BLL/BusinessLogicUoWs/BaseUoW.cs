using System;
using Task_4.DAL.Abstractions;

namespace Task_4.BLL.BusinessLogicUoWs
{
    public class BaseUoW<TEntity> : IDisposable where TEntity : class
    {
        private bool _disposedValue;

        public IGenericRepository<TEntity> Repository { get; protected set; }
        public BaseUoW(IGenericRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (!disposing) return;
            Repository?.Dispose();
            Repository = null;
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

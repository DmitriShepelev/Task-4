using System;
using System.Collections.Generic;
using System.Threading;
using Task_4.Persistence.Models;

namespace Task_4.BLL.Infrastructure
{
    public class ConcurrencyLockProvider : IDisposable
    {
        private readonly ReaderWriterLockSlim _clientLocker = new();
        private readonly ReaderWriterLockSlim _managerLocker = new();
        private readonly ReaderWriterLockSlim _productLocker = new();
        private readonly Dictionary<Type, ReaderWriterLockSlim> _internalDictionary;

        public ConcurrencyLockProvider()
        {
            _internalDictionary = new Dictionary<Type, ReaderWriterLockSlim>
            {
                { typeof(Client), new ReaderWriterLockSlim() },
                { typeof(Manager), new ReaderWriterLockSlim() },
                { typeof(Product), new ReaderWriterLockSlim() }
            };
        }

        public ReaderWriterLockSlim GetLock<TEntity>() where TEntity : class
        {
            return _internalDictionary[typeof(TEntity)];
        }

        public void Dispose()
        {
            _clientLocker?.Dispose();
            _managerLocker?.Dispose();
            _productLocker?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}

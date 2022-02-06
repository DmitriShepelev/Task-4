using System;
using System.Collections.Generic;
using System.Threading;
using Task_4.Models;

namespace Task_4.BLL.Infrastructure
{
    public class ConcurrencyLockProvider 
    {
        private readonly ReaderWriterLockSlim _clientLocker = new();
        private readonly ReaderWriterLockSlim _managerLocker = new();
        private readonly ReaderWriterLockSlim _productLocker = new();
        private readonly Dictionary<Type, ReaderWriterLockSlim> _internalDictionary;

        public ConcurrencyLockProvider()
        {
            _internalDictionary = new Dictionary<Type, ReaderWriterLockSlim>
            {
                { typeof(Client), _clientLocker },
                { typeof(Manager), _managerLocker },
                { typeof(Product), _productLocker }
            };
        }

        public ReaderWriterLockSlim GetLock<TEntity>() where TEntity : class
        {
            return _internalDictionary[typeof(TEntity)];
        }
    }
}

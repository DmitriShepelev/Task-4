using System;
using System.Transactions;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;

namespace Task_4.BLL.Handlers
{
    public class DataSourceHandler<TDto> : IDataSourceHandler
    {
        protected IFileDataSource<TDto> DataSource { get;  }
        protected IDataItemHandler<TDto> ItemHandler { get; }
        protected IDbConnectionHandler DbConnectionHandler { get; }

        public DataSourceHandler(IFileDataSource<TDto> dataSource,
            IDataItemHandler<TDto> itemHandler,
            IDbConnectionHandler dbConnectionHandler)
        {
            DataSource = dataSource;
            ItemHandler = itemHandler;
            DbConnectionHandler = dbConnectionHandler;
        }
        
        public void Run()
        {
            try
            {
                foreach (var order in DataSource)
                {
                    ItemHandler.SaveOrder(order);
                }

                using var scope = CreateTransaction();
                DbConnectionHandler.Commit(!DataSource.SessionCompleted);
                DataSource.MoveToProcessed();
                scope.Complete();
            }
            catch (Exception)
            {
                DbConnectionHandler.Rollback(!DataSource.SessionCompleted);
            }
        }

        protected TransactionScope CreateTransaction()
        {
            return new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ItemHandler?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

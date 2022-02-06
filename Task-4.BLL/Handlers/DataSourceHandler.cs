using System;
using System.Transactions;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.Handlers
{
    public class DataSourceHandler<TDto> : IDataSourceHandler
    {
        protected IFileDataSource<TDto> DataSource { get; }
        protected IDataItemHandler<TDto> ItemHandler { get; }

        public DataSourceHandler(IFileDataSource<TDto> dataSource, IDataItemHandler<TDto> itemHandler)
        {
            DataSource = dataSource;
            ItemHandler = itemHandler;
        }

        public void Run()
        {
            using var scope = CreateTransaction();
            try
            {
                foreach (var order in DataSource)
                {
                    ItemHandler.SaveOrder(order);
                }
                
                DataSource.MoveToProcessed();
            }
            catch (Exception e)
            {
                throw new HandlerException(e, e.Message);
            }
            finally
            {
                Dispose();
            }
            scope.Complete();
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

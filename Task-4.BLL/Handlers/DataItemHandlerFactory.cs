using Task_4.BLL.Abstractions;
using Task_4.BLL.BusinessLogicUoWs;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;
using Task_4.DAL.Contexts;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;

namespace Task_4.BLL.Handlers
{
    public class DataItemHandlerFactory : IDataItemHandlerFactory<DataSourceDto>
    {
        private readonly string _connectionString;
        protected ConcurrencyLockProvider ConcurrencyLock;

        public DataItemHandlerFactory(ConcurrencyLockProvider concurrencyLock, string connectionString)
        {
            ConcurrencyLock = concurrencyLock;
            _connectionString = connectionString;
        }

        public IDataItemHandler<DataSourceDto> CreateInstance()
        {
            var context = new ApplicationContext();
            
            var clientRepo = new GenericRepository<Client>(context);
            var managerRepo = new GenericRepository<Manager>(context);
            var productRepo = new GenericRepository<Product>(context);
            var orderRepo = new GenericRepository<Order>(context);
            return new DataItemHandler<DataSourceDto>(new DtoParser(),
                new UpsertUoW<Client>(clientRepo, ConcurrencyLock),
                new UpsertUoW<Manager>(managerRepo, ConcurrencyLock),
                new UpsertUoW<Product>(productRepo, ConcurrencyLock),
                new AddUoW<Order>(orderRepo));
        }
    }
}

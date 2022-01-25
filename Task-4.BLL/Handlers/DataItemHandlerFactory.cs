using System.Data.SqlClient;
using Task_4.BLL.Abstractions;
using Task_4.BLL.BusinessLogicUoWs;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;
using Task_4.Contexts;
using Task_4.DAL.Repositories;
using Task_4.Models;

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
            var context = new Task4Context(new SqlConnection(_connectionString));
            
            var clientRepo = new Task4GenericRepository<Client>(context);
            var managerRepo = new Task4GenericRepository<Manager>(context);
            var productRepo = new Task4GenericRepository<Product>(context);
            var orderRepo = new Task4GenericRepository<Order>(context);
            return new DataItemHandler<DataSourceDto>(new DtoParser(),
                new UpsertUoW<Client>(clientRepo, ConcurrencyLock),
                new UpsertUoW<Manager>(managerRepo, ConcurrencyLock),
                new UpsertUoW<Product>(productRepo, ConcurrencyLock),
                new AddUoW<Order>(orderRepo));
        }
    }
}

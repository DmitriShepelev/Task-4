using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.BusinessLogicUoWs;
using Task_4.BLL.Handlers;
using Task_4.BLL.Infrastructure;
using Task_4.DAL.Abstractions;
using Task_4.Persistence.Models;

namespace Task_4.BLL.Factories
{
    public class DataItemHandlerFactory : IDataItemHandlerFactory<DataSourceDto>
    {
        protected IRepositoryFactory RepositoryFactory;
        protected IContextFactory ContextFactory;
        protected ConcurrencyLockProvider ConcurrencyLock;
        private readonly IDtoParserFactory<DataSourceDto> _parserFactory;

        public DataItemHandlerFactory(IRepositoryFactory repositoryFactory, IContextFactory contextFactory, ConcurrencyLockProvider concurrencyLock, IDtoParserFactory<DataSourceDto> parserFactory)
        {
            RepositoryFactory = repositoryFactory;
            ContextFactory = contextFactory;
            ConcurrencyLock = concurrencyLock;
            _parserFactory = parserFactory;
        }

        public IDataItemHandler<DataSourceDto> CreateInstance()
        {
            var context = ContextFactory.CreateInstance();

            var clientRepo = RepositoryFactory.CreateInstance<Client>(context);
            var managerRepo = RepositoryFactory.CreateInstance<Manager>(context);
            var productRepo = RepositoryFactory.CreateInstance<Product>(context);
            var orderRepo = RepositoryFactory.CreateInstance<Order>(context);
            return new DataItemHandler<DataSourceDto>(_parserFactory.CreateInstance(),
                new UpsertUoW<Client>(clientRepo, ConcurrencyLock),
                new UpsertUoW<Manager>(managerRepo, ConcurrencyLock),
                new UpsertUoW<Product>(productRepo, ConcurrencyLock),
                new AddUoW<Order>(orderRepo));
        }
    }
}

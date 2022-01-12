using System.Data.Common;
using System.Data.Entity;
using Task_4.DAL.Abstractions;

namespace Task_4.DAL.ContextFactories
{
    public class Task4ContextFactory : IContextFactory
    {
        private readonly IConnectionFactory _connectionFactory;

        public Task4ContextFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public DbContext CreateInstance(DbConnection connection = null, bool contextOwnsConnection = true)
        {
            var temp = connection ?? _connectionFactory.CreateInstance();
            return new DbContext(temp, contextOwnsConnection);
        }
    }
}

using System;
using System.Data.Common;
using System.Data.Entity;
using Task_4.DAL.Abstractions;
using Task_4.Persistence.Contexts;

namespace Task_4.DAL.ContextFactories
{
    public class Task4ContextFactory : IContextFactory
    {
        private readonly IConnectionFactory _connectionFactory;

        public Task4ContextFactory(IConnectionFactory connectionFactory = null)
        {
            _connectionFactory = connectionFactory;
        }
        public DbContext CreateInstance(DbConnection connection = null, bool contextOwnsConnection = true)
        {
            var temp = connection ?? _connectionFactory.CreateInstance();
            if (temp == null) throw new InvalidOperationException("No connection object can be substituted");
            var context = new Task4Context(temp, contextOwnsConnection);
            context.Database.CreateIfNotExists();
            return context;
        }
    }
}

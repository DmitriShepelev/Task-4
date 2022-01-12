using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.DAL.Abstractions;
using Task_4.Persistence.Models;

namespace Task_4.BLL.Handlers
{
   public class DbConnectionHandler : IDbConnectionHandler
    {
        public DbConnectionHandler(IContextFactory contextFactory,
            IRepositoryFactory repositoryFactory,
            IConnectionFactory connectionFactory)
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
            ConnectionFactory = connectionFactory;
        }

        private IContextFactory ContextFactory { get; }
        private IRepositoryFactory RepositoryFactory { get; }
        private IConnectionFactory ConnectionFactory { get; }

        public void Commit(bool sessionCompletedState)
        {
            UseClosure(
                (context, repository) =>
                {
                    var orders = repository.Get(x => !x.SessionCompleted);
                    foreach (var order in orders)
                    {
                        order.SessionCompleted = true;
                        repository.Update(order);
                    }

                    context.SaveChanges();
                }
                );
        }

        public void Rollback(bool sessionCompletedState)
        {
            UseClosure(
                (context, repository) =>
                {
                    var orders = repository.Get(x => !x.SessionCompleted);
                    foreach (var order in orders)
                    {
                        repository.Remove(order);
                    }

                    context.SaveChanges();
                }
            );
        }

        protected void UseClosure(Action<DbContext, IGenericRepository<Order>> action)
        {
            DbContext context = null;
            IGenericRepository<Order> repository = null;
            try
            {
                CreateClose(out context, out repository);
                action?.Invoke(context, repository);
            }
            catch (Exception e)
            {
                throw new HandlerException(e);
            }
            finally
            {
                if (repository != null) repository.Dispose();
                if (context != null) context.Dispose();
            }
        }

        protected void CreateClose(out DbContext context, out IGenericRepository<Order> repository)
        {
            context = ContextFactory.CreateInstance(ConnectionFactory.CreateInstance());
            repository = RepositoryFactory.CreateInstance<Order>(context);
        }

    }
}

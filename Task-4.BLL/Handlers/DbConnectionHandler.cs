//using System;
//using System.Data.Entity;
//using System.Data.SqlClient;
//using Task_4.BLL.Abstractions;
//using Task_4.Contexts;
//using Task_4.DAL.Abstractions;
//using Task_4.DAL.Repositories;
//using Task_4.Models;

//namespace Task_4.BLL.Handlers
//{
//   public class DbConnectionHandler : IDbConnectionHandler
//    {
//        public DbConnectionHandler(//IContextFactory contextFactory,
//            //IRepositoryFactory repositoryFactory,
//            //IConnectionFactory connectionFactory,
//            string connectionString)
//        {
//            //ContextFactory = contextFactory;
//            //RepositoryFactory = repositoryFactory;
//            //ConnectionFactory = connectionFactory;
//            _connectionString = connectionString;
//        }

//        //private IContextFactory ContextFactory { get; }
//        //private IRepositoryFactory RepositoryFactory { get; }
//        //private IConnectionFactory ConnectionFactory { get; }
//        private readonly string _connectionString;

//        public void Commit(bool sessionCompletedState)
//        {
//            //UseClosure(
//            //    (context, repository) =>
//            //    {
//            //        var orders = repository.Get(x => !x.SessionCompleted);
//            //        foreach (var order in orders)
//            //        {
//            //            order.SessionCompleted = true;
//            //            repository.Update(order);
//            //        }

//            //        context.SaveChanges();
//            //    }
//            //    );
//        }

//        public void Rollback(bool sessionCompletedState)
//        {
//            UseClosure(
//                (context, repository) =>
//                {
//                    var orders = repository.Get(x => !x.SessionCompleted);
//                    foreach (var order in orders)
//                    {
//                        repository.Remove(order);
//                    }

//                    context.SaveChanges();
//                }
//            );
//        }

//        protected void UseClosure(Action<DbContext, IGenericRepository<Order>> action)
//        {
//            DbContext context = null;
//            IGenericRepository<Order> repository = null;
//            try
//            {
//                CreateClose(out context, out repository);
//                action?.Invoke(context, repository);
//            }
//            catch (Exception e)
//            {
//                throw new HandlerException(e);
//            }
//            finally
//            {
//                repository?.Dispose();
//                context?.Dispose();
//            }
//        }

//        protected void CreateClose(out DbContext context, out IGenericRepository<Order> repository)
//        {
//            //context = ContextFactory.CreateInstance(ConnectionFactory.CreateInstance());
//            context = new Task4Context(new SqlConnection(_connectionString));
//            //repository = RepositoryFactory.CreateInstance<Order>(context);
//            repository = new Task4GenericRepository<Order>(context);
//        }

//    }
//}

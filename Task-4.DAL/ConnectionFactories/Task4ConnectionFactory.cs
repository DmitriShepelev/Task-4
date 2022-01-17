using System.Data.Common;
using System.Data.SqlClient;
using Task_4.DAL.Abstractions;

namespace Task_4.DAL.ConnectionFactories
{
   public class Task4ConnectionFactory : IConnectionFactory
   {
       private readonly string _connectionString;

       public Task4ConnectionFactory(string connectionString)
       {
           _connectionString = connectionString;
        }
        public DbConnection CreateInstance(bool openOnCreate = false)
        {
            var connection = new SqlConnection(_connectionString);
            if (openOnCreate) connection.Open();
            return connection;
        }
    }
}

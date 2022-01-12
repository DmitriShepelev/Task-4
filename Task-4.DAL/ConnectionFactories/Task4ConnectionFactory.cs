using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

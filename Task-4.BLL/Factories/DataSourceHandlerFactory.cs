using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.Handlers;

namespace Task_4.BLL.Factories
{
   public class DataSourceHandlerFactory : IDataSourceHandlerFactory<DataSourceDto>
   {
       private readonly IDbConnectionHandler _dbConnectionHandler;
       private readonly IDataItemHandlerFactory<DataSourceDto> _dataItemHandlerFactory;

       public DataSourceHandlerFactory(IDbConnectionHandler dbConnectionHandler,
           IDataItemHandlerFactory<DataSourceDto> dataItemHandlerFactory)
       {
           _dbConnectionHandler = dbConnectionHandler;
           _dataItemHandlerFactory = dataItemHandlerFactory;
       }

       public IDataSourceHandler CreateInstance(IFileDataSource<DataSourceDto> source)
       {
           return new DataSourceHandler<DataSourceDto>(
               source,
               _dataItemHandlerFactory.CreateInstance(),
               _dbConnectionHandler);
        }
   }
}

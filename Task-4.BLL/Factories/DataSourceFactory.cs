//using Task_4.BLL.Abstractions;
//using Task_4.BLL.Abstractions.Factories;
//using Task_4.BLL.DataSources;
//using Task_4.BLL.Infrastructure;

//namespace Task_4.BLL.Factories
//{
//    public class DataSourceFactory : IDataSourceFactory<DataSourceDto>
//    {
//        private readonly AppOptions _options;
//        public DataSourceFactory(AppOptions options)
//        {
//            _options = options;
//        }

//        public IFileDataSource<DataSourceDto> CreateInstance(string fileName)
//        {
//            return new FileDataSource(fileName, _options.Target);
//        }

//    }
//}

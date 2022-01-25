//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Task_4.BLL.Abstractions;
//using Task_4.BLL.Abstractions.Factories;
//using Task_4.BLL.Infrastructure;

//namespace Task_4.BLL.DataSources
//{
//    public class FolderDataSource : IEnumerable<IFileDataSource<DataSourceDto>>
//    {
//        private readonly AppOptions _appOptions;
//        private readonly IDataSourceFactory<DataSourceDto> _dataSourceFactory;

//        public FolderDataSource(AppOptions appOptions, IDataSourceFactory<DataSourceDto> dataSourceFactory)
//        {
//            _appOptions = appOptions;
//            _dataSourceFactory = dataSourceFactory;
//        }

//        public IEnumerator<IFileDataSource<DataSourceDto>> GetEnumerator()
//        {
//            var files = Directory.GetFiles(_appOptions.Source, _appOptions.Pattern);
//            foreach (var fileName in files)
//            {
//                yield return _dataSourceFactory.CreateInstance(fileName);
//            }
//        }
//        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
//    }
//}

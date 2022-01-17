using System.Collections;
using System.Collections.Generic;
using System.IO;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.DataSources
{
    public class FolderDataSource : IEnumerable<IFileDataSource<DataSourceDto>>
    {
        private readonly AppFolderOptions _appFolderOptions;
        private readonly IDataSourceFactory<DataSourceDto> _dataSourceFactory;

        public FolderDataSource(AppFolderOptions appFolderOptions, IDataSourceFactory<DataSourceDto> dataSourceFactory)
        {
            _appFolderOptions = appFolderOptions;
            _dataSourceFactory = dataSourceFactory;
        }

        public IEnumerator<IFileDataSource<DataSourceDto>> GetEnumerator()
        {
            var files = Directory.GetFiles(_appFolderOptions.Source, _appFolderOptions.Pattern);
            foreach (var fileName in files)
            {
                yield return _dataSourceFactory.CreateInstance(fileName);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}

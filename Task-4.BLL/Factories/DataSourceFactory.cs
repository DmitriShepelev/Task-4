using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.Factories
{
    public class DataSourceFactory : IDataSourceFactory<DataSourceDto>
    {
        private readonly AppFolderOptions _folderOptions;
        public DataSourceFactory(AppFolderOptions folderOptions)
        {
            _folderOptions = folderOptions;
        }

        public IFileDataSource<DataSourceDto> CreateInstance(string fileName)
        {
            return new FileDataSource(fileName, _folderOptions.Target);
        }

    }
}

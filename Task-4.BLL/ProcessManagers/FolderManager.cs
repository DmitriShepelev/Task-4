using System;
using System.Collections.Generic;
using System.IO;
using Task_4.BLL.Abstractions;
using Task_4.BLL.DataSources;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.ProcessManagers
{
    public class FolderManager<TDtoEntity> : BaseFileManager<TDtoEntity>
    {
        private readonly AppOptions _appOptions;
        public FolderManager(IDataItemHandlerFactory<TDtoEntity> dataItemHandlerFactory, AppOptions appOptions) : base(dataItemHandlerFactory)
        {
            _appOptions = appOptions;
        }

        public override void StartProcess(Action<IFileDataSource<TDtoEntity>> processAction)
        {
            foreach (var fileDataSource in GetFiles())
            {
                processAction(fileDataSource);
            }
        }

        private IEnumerable<IFileDataSource<TDtoEntity>> GetFiles()
        {
            var files = Directory.GetFiles(_appOptions.Source, _appOptions.Pattern);
            foreach (var fileName in files)
            {
                yield return (IFileDataSource<TDtoEntity>)new FileDataSource(fileName, _appOptions.Target);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Task_4.BLL.Abstractions;

namespace Task_4.BLL.DataSources
{
    public class FileDataSource : IFileDataSource<DataSourceDto>
    {
        private readonly string _sourceFileName;
        private readonly string _targetPath;
        private readonly string _managerName;
        public bool SessionCompleted => false;

        public FileDataSource(string sourceFileName, string targetPath)
        {
            _sourceFileName = sourceFileName;
            _targetPath = targetPath;
            _managerName = GetManagerName();
        }

        private string GetManagerName()
        {
            var x = _sourceFileName.Split('_')[0];
            var y = x.Split('\\')[^1].ToString();
            return y;

        }

        public void MoveToProcessed()
        {
            var target = string.Concat(_targetPath, Path.GetFileName(_sourceFileName));
            if (_sourceFileName != null) File.Move(_sourceFileName, target);
        }

        public IEnumerator<DataSourceDto> GetEnumerator()
        {
            using StreamReader reader = new(_sourceFileName);
            var currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                var items = currentLine.Trim().Split(';', StringSplitOptions.RemoveEmptyEntries);
                yield return new DataSourceDto()
                {
                    ManagerSecondName = _managerName,
                    PurchaseDate = items[0],
                    ClientName = items[1],
                    ProductName = items[2],
                    Amount = Convert.ToDecimal(items[3]),
                    SessionCompleted = SessionCompleted
                };
                currentLine = reader.ReadLine();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

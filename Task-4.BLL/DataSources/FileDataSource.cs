using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ChinhDo.Transactions;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Infrastructure;

namespace Task_4.BLL.DataSources
{
    public class FileDataSource : IFileDataSource<DataSourceDto>
    {
        private readonly Index _fileNameIndex = new(1, true);
        private const int PurchaseDateIndex = 0;
        private const int ClientNameIndex = 1;
        private const int ProductNameIndex = 2;
        private const int AmountIndex = 3;
        private readonly string _sourceFilePath;
        private readonly string _targetFolderPath;
        private readonly TxFileManager _fileManager = new();

        public FileDataSource(string sourceFilePath, string targetFolderPath)
        {
            _sourceFilePath = sourceFilePath;
            _targetFolderPath = targetFolderPath;
        }
        
        public void MoveToProcessed()
        {
            var target = string.Concat(_targetFolderPath, Path.GetFileName(_sourceFilePath));
            _fileManager.Move(_sourceFilePath, target);
        }

        public string FileName  => _sourceFilePath.Split('\\')[_fileNameIndex];

        public IEnumerator<DataSourceDto> GetEnumerator()
        {
            using StreamReader reader = new(_sourceFilePath);
            var currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                var items = currentLine.Trim().Split(';', StringSplitOptions.RemoveEmptyEntries);
                yield return new DataSourceDto()
                {
                    ManagerSecondName = FileName.Split('_')[0],
                    PurchaseDate = items[PurchaseDateIndex],
                    ClientName = items[ClientNameIndex],
                    ProductName = items[ProductNameIndex],
                    Amount = Convert.ToDecimal(items[AmountIndex])
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

﻿using System;
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
        private readonly string _sourceFileName;
        private readonly string _targetPath;
        private readonly TxFileManager _fileManager = new();

        public FileDataSource(string sourceFileName, string targetPath)
        {
            _sourceFileName = sourceFileName;
            _targetPath = targetPath;
        }
        
        public void MoveToProcessed()
        {
            var target = string.Concat(_targetPath, Path.GetFileName(_sourceFileName));
            _fileManager.Move(_sourceFileName, target);
        }

        public string FileName  => _sourceFileName.Split('\\')[^1];

        public IEnumerator<DataSourceDto> GetEnumerator()
        {
            using StreamReader reader = new(_sourceFileName);
            var currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                var items = currentLine.Trim().Split(';', StringSplitOptions.RemoveEmptyEntries);
                yield return new DataSourceDto()
                {
                    ManagerSecondName = GetManagerName(),
                    PurchaseDate = items[0],
                    ClientName = items[1],
                    ProductName = items[2],
                    Amount = Convert.ToDecimal(items[3])
                };
                currentLine = reader.ReadLine();
            }
        }

        private string GetManagerName()
        {
            var x = _sourceFileName.Split('_')[0];
            var y = x.Split('\\')[^1];
            return y;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

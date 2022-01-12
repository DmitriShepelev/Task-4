using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    // источник данных
    public interface IFileDataSource<DTOEntity> : IEnumerable<DTOEntity>
    {
        // операция перемещения обработанных файлов
        void MoveToProcessed();
        bool SessionCompleted { get; }
    }
}

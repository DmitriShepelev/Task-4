using System.Collections.Generic;

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

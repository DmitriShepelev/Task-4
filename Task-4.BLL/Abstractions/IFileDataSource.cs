using System.Collections.Generic;

namespace Task_4.BLL.Abstractions
{
    public interface IFileDataSource<TDtoEntity> : IEnumerable<TDtoEntity>
    {
        void MoveToProcessed();
        string FileName { get; }
    }
}

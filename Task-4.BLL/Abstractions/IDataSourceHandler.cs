using System;

namespace Task_4.BLL.Abstractions
{
    public interface IDataSourceHandler : IDisposable
    {
        void Run();
    }
}

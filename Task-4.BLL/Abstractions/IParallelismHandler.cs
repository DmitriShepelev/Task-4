using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions
{
    public interface IParallelismHandler
    {
        CancellationTokenSource CancellationTokenSource { get; }
        CancellationTokenSource StopTokenSource { get; }
        TaskScheduler TaskScheduler { get; }
        void Add(Task task);
        void Remove(Task task);
        Task RequestCancel();
        Task RequestStop();
        Task WaitForCompletion();
        bool TryGetLock();
        void ExitLock();
    }
}

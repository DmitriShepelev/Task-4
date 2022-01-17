using System.IO;
using System.Threading;

namespace Task_4.BLL.Infrastructure
{
    public class Watcher
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ManualResetEvent _stopThreadEvent = new(false);

        public Watcher(FileSystemWatcher watcher)
        {
            _watcher = watcher;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            _stopThreadEvent.Reset();
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _stopThreadEvent.Set();
        }



        public event FileSystemEventHandler ActionConnector
        {
            add => _watcher.Created += value;
            remove => _watcher.Created -= value;
        }

        public void WaitForStop()
        {
            _stopThreadEvent.WaitOne();
        }
    }
}

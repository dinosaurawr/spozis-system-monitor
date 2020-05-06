using System;
using System.Threading;
using ServerMonitorFirst;
using Shared;

namespace ServerMonitorSecond
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new MappedFileManager("monitorFile");
            var fileChangedEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "MonitorFileChanged");
            while (true)
            {
                var message = new MessageModel()
                {
                    MessageTime = DateTime.Now.ToBinary(),
                    ServerNumber = 2,
                    UptimeSeconds = SystemMonitorManager.UpTime.TotalSeconds,
                    CurrentProcessPriority = SystemMonitorManager.CurrentProcessPriority,
                    CurrentSystemTime = SystemMonitorManager.CurrentSystemTime.ToBinary(),
                    ServerProcessPriority = 1
                };
                
                fileManager.WriteMessageToFile(message);
                Console.WriteLine("Send message:");
                Console.WriteLine(message);
                fileChangedEvent.Set();

                Thread.Sleep(5000);
            }
        }
    }
}
using System;
using System.Threading;
using Shared;

namespace ClientMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new MappedFileManager("monitorFile");
            var fileChangedEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "MonitorFileChanged");

            while (true)
            {
                fileChangedEvent.WaitOne();
                var message = fileManager.ReadLastMessageFromMappedFile();
                Console.WriteLine("Received message");
                Console.WriteLine(message);
                Thread.Sleep(500);
            }
        }
    }
}
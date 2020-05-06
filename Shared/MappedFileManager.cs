using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using ServerMonitorFirst;

namespace Shared
{
    public class MappedFileManager : IDisposable
    {
        private readonly MemoryMappedFile _mappedFile;
        private readonly long _lastReadOffset = 0;
        
        public MappedFileManager(string memoryMappedName)
        {
            _mappedFile = MemoryMappedFile.CreateOrOpen(memoryMappedName, 1024);
        }

        public void WriteMessageToFile(MessageModel message)
        {
            Mutex mutex;
            if (!Mutex.TryOpenExisting("monitorFileMutex", out mutex))
            {
                mutex = new Mutex(false, "monitorFileMutex");
            }

            mutex.WaitOne();
            using var stream = _mappedFile.CreateViewStream();
            var writer = new BinaryWriter(stream);
            writer.Write(message.Serialize());
            mutex.ReleaseMutex();
        }
        
        public MessageModel ReadLastMessageFromMappedFile()
        {
            Mutex mutex;
            if (!Mutex.TryOpenExisting("monitorFileMutex", out mutex))
            {
                mutex = new Mutex(false, "monitorFileMutex");
            }

            mutex.WaitOne();
            var size = MessageModelExtensions.GetMessageModelSize();
            using var stream = _mappedFile.CreateViewStream(_lastReadOffset, size);
            var reader = new BinaryReader(stream);
            var bytes = reader.ReadBytes(size);
            var result = bytes.Deserialize<MessageModel>();
            mutex.ReleaseMutex();
            return result;
        }

        public void Dispose()
        {
            _mappedFile.Dispose();
        }
    }
}
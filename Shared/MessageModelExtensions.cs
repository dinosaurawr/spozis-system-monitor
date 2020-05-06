using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ServerMonitorFirst;

namespace Shared
{
    public static class MessageModelExtensions
    {
        public static int GetMessageModelSize()
        {
            return new MessageModel().Serialize().Length;
        }
        
        public static byte[] Serialize(this MessageModel messageModel)
        {
            using var memoryStream = new MemoryStream();
            IFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, messageModel);
            return memoryStream.ToArray();
        }

        public static T Deserialize<T>(this byte[] byteArray)
        {   
            T returnValue;
            using var memoryStream = new MemoryStream(byteArray);
            IFormatter binaryFormatter = new BinaryFormatter();
            returnValue = (T)binaryFormatter.Deserialize(memoryStream);
            return returnValue;
        }
    }
}
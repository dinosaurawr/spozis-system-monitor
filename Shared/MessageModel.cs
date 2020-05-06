using System;
using System.Text;

namespace Shared
{
    [Serializable]
    public struct MessageModel
    {
        public long MessageTime;
        public int ServerNumber;
        public long CurrentSystemTime;
        public double UptimeSeconds;
        public int CurrentProcessPriority;
        public int ServerProcessPriority;
        

        public override string ToString()
        {
            var uptimeTimeSpan = TimeSpan.FromSeconds(UptimeSeconds);
            var stringBuilder = new StringBuilder()
                .AppendLine($"[{DateTime.FromBinary(MessageTime).ToString()}] FromServer: {ServerNumber}")
                .AppendLine($"Current system time: {DateTime.FromBinary(CurrentSystemTime)}")
                .AppendLine(
                    $"Uptime: ${uptimeTimeSpan.Days}d:{uptimeTimeSpan.Hours}h:{uptimeTimeSpan.Minutes}:{uptimeTimeSpan.Seconds}s")
                .AppendLine($"Current precess priority: {CurrentProcessPriority}")
                .AppendLine($"Server process priority: {ServerProcessPriority}");

            return stringBuilder.ToString();
        }
    }
}
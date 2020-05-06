using System;
using System.Diagnostics;

namespace ServerMonitorFirst
{
    public static class SystemMonitorManager
    {
        public static DateTime CurrentSystemTime => DateTime.Now;

        public static TimeSpan UpTime 
        {
            get
            {
                var ticks = Stopwatch.GetTimestamp();
                var uptime = ((double) ticks) / Stopwatch.Frequency;
                return TimeSpan.FromSeconds(uptime);
            }
        }
        
        public static int CurrentProcessPriority => Process.GetCurrentProcess().BasePriority;
    }
}
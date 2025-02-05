using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    public static class Logger
    {
        private static readonly List<string> _logEntries = new List<string>();
        private static readonly object _lock = new object();
        public static void LogInfo(string message) => Log("INFO", message);
        public static void LogWarning(string message) => Log("WARNING", message);
        public static void LogError(string message) => Log("ERROR", message);
        public static void LogAttribute(string message) => Log("ATTRIBUTE", message);

        public static HashSet<string> attributeSet = new HashSet<string>();

        private static void Log(string level, string message)
        {
            if(level == "ATTRIBUTE") // prevent the log from flooding with attribute messages
            {
                if( attributeSet.Contains(message))
                {
                    return;
                }
                else
                {
                    attributeSet.Add(message);
                }
            }

            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            lock (_lock)
            {
                _logEntries.Add(logEntry);
            }
        }

        public static IEnumerable<string> GetLogs()
        {
            lock (_lock)
            {
                return new List<string>(_logEntries); // Return a copy to avoid modification issues
            }
        }
    }
}





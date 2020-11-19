using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaStore
{
    public interface ILoggerRepo
    {
        void AddToLogs(Log log);

        List<Log> GetAllLogs();
    }

    public class LoggerRepo : ILoggerRepo
    {
        public void AddToLogs(Log log)
        {
            LoggerStore.Logs.Add(log);
        }

        public List<Log> GetAllLogs()
        {
            return LoggerStore.Logs;
        }
    }

    public class LoggerStore
    {
        public static List<Log> Logs = new List<Log>();
    }
}

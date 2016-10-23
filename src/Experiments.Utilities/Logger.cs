using System;
using System.IO;
using System.Reflection;

namespace Experiments.Utilities
{
    public static class Logger
    {
        private static readonly string logFileName = "CarSpawn.log";
        public static void Log(object message)
        {
            string appName =
                Assembly.GetCallingAssembly().GetName().Name;

            File.AppendAllText(logFileName,
                $"{DateTime.Now} - {appName} : {message}{Environment.NewLine}");
        }
    }
}

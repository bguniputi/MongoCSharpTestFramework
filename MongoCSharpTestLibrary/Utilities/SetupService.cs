using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenTestLibrary.Utilities
{
    public static class SetupService
    {
        private static readonly object lockObj = new object();

        private static string _logFilePath;
        /// <summary>
        /// Default constructor with log file initialization
        /// </summary>
        static SetupService()
        {
            _logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
        }

        private static void LoadConfiguration()
        {
            lock (lockObj)
            {
                
            }
        }
        /// <summary>
        /// Get log file path
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                if (_logFilePath == null)
                {
                    _logFilePath = @"C:\AUT\ExecutionLogs\NextGenTestLog-{Data}.txt";
                }

                return _logFilePath;
            }
        }
    }
}

using NextGenTestLibrary.Utilities;
using Serilog;

namespace NextGenTestLibrary.Loggers
{
    public class Logger
    {
        /// <summary>
        /// Global log object
        /// </summary>
        public static Serilog.Core.Logger log = new LoggerConfiguration()
                                           .ReadFrom
                                           .AppSettings()
                                           .WriteTo
                                           .Async(a => a.RollingFile(SetupService.LogFilePath))
                                           .CreateLogger();

    }
}

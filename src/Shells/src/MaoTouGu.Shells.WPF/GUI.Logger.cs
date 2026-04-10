using NLog;
using NLog.Config;
using NLog.Targets;

namespace MaoTouGu.Shells
{
    partial class GUI
    {
         //-------------------------------------------------------------
        //
        //          Logger
        //
        //-------------------------------------------------------------
        public static ILogger InstallLogger(string dir)
        {
            var config = new LoggingConfiguration();

            var logfile = new FileTarget("logfile")
            {
                FileName = dir + "/${shortdate}.log",
                Layout   = @"${level}：${date:HH\:mm\:ss} | ${logger} ${message} ",
            };
            
            // Danger：ViewModel-Danger

            var debugFileTarget = new DebuggerTarget
            {
                Layout   = @"${level}：${date:HH\:mm\:ss} | ${logger} ${message} ",
            };
            
            // {callsite}
            // {callsite-linenumber}
            // 
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, debugFileTarget);


            LogManager.Configuration = config;
            return LogManager.GetLogger("App");
        }
        
        public static ILogger InstallAndroidLogger()
        {
            var config = new LoggingConfiguration();


            var debugFileTarget = new DebuggerTarget
            {
                Layout   = @"${level}：${date:HH\:mm\:ss} | ${logger} ${message} ",
            };
            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, debugFileTarget);


            LogManager.Configuration = config;
            return LogManager.GetLogger("App");
        }

        public static void AppendLoggerTarget(string dir)
        {
            var config = LogManager.Configuration;
            
            var logfile = new FileTarget("logfile")
            {
                FileName = dir + "/${shortdate}.log",
                Layout   = @"${level}：${date:HH\:mm\:ss} | ${logger} ${message} ",
            };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
        }
    }
}
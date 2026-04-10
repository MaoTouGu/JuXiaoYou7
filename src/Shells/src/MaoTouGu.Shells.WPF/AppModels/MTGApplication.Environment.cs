using System.ComponentModel;
using System.Windows.Threading;
using MaoTouGu.Shells.Languages;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MaoTouGu.Shells.AppModels
{
    partial class MTGApplication
    {
        protected MTGApplication()
        {
            RegisterForwardServices();
            BuildAppHostImpl();
        }

        private void BuildAppHostImpl()
        {
            var impl   = new AppConfigBuilder();
            var config = BuildAppHost(impl);
            
            //
            //
            foreach (var provider in config.Languages)
            {
                I18N.SetLanguage(provider);
            }

            //
            // Install Languages
            I18N.LCID     = config.LCID;
            IsCustomTheme = impl.AppConfig.Theme == AppTheme.Custom;
            IsDarkTheme   = impl.AppConfig.Theme == AppTheme.Dark;
            
            //
            // Install Loggers
            _logger = InstallLogger(config.DirOfLogs);
            
            //
            // Install Exception Handler
            InstallExceptionHandler();
        }
        
        //-------------------------------------------------------------
        //
        //                          Logger
        //
        //-------------------------------------------------------------
        static ILogger InstallLogger(string dir)
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
        
        private void InstallExceptionHandler()
        {
            DispatcherUnhandledException               += OnUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        //-------------------------------------------------------------
        //
        //                     Handle OnUnhandledException
        //
        //-------------------------------------------------------------
        #region OnUnhandledException

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Warn(e.ExceptionObject.ToString());
        }

        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = OnUnhandledException(e.Exception);
            
            if (e.Handled)
            {
                return;
            }
            
            
            e.Handled = true;
            Logger.Warn(e.Exception.Message);
        }

        protected virtual bool OnUnhandledException(Exception exception) => false;

        #endregion
        
        //-------------------------------------------------------------
        //
        //                     Override Methods
        //
        //-------------------------------------------------------------
        protected abstract IAppConfig BuildAppHost(IAppConfigBuilder builder);
        
        public bool IsCustomTheme { get; private set; }
        public bool IsDarkTheme { get; private set; }
    }
}
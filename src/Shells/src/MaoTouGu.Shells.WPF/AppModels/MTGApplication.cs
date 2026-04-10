

using NLog;

namespace MaoTouGu.Shells.AppModels
{
    /// <summary>
    /// <see cref="MTGApplication"/> 类型用于猫头菇工作室的Application封装。
    /// </summary>
    public abstract partial class MTGApplication : Application
    {
        private ILogger _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            GUI.SetSynchronizationContext();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected ILogger Logger => _logger;
    }
}
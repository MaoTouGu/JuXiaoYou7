using MaoTouGu.JuXiaoYou.Bootstrap.FeatureExplores;
using MaoTouGu.JuXiaoYou.Classifiers;
using MaoTouGu.JuXiaoYou.Domain.IM.Pages;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Services.Plugins;
using MaoTouGu.Studio.Database.Utils;
using MaoTouGu.Teams;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public class ChatShell : ShellBase<MainWindow, StandaloneWindow>
    {
        protected sealed override void OnStart()
        {
            Navigate<StartupViewModel>();
        }

        private DialogHost FindDialogHost()
        {
            return WindowTable.Values
                              .FirstOrDefault(x => x.IsActivate && x.DialogHost is not null)
                              ?.DialogHost;
        }

        private void ClosePlaceholdingView()
        {
            
            if (InstanceTable.Values
                             .FirstOrDefault(x => x.ViewModel is PlaceholdingViewModel) is
                {
                    ViewModel:  PlaceholdingViewModel landing
                })
            {
                landing.Stop();
            }
        }

        protected override void OnStartup()
        {
            //
            //
            Navigate<LobbyViewModel>();
            
            
            
            //
            // Navigate
            new SpriteWindow().Show();
        }

        #region JumpTask

        public static JumpTask CreateNavigateToSettingTask()
        {
            var setting = Ioc.Get<IAppConfig>().DirOfSettings;

            return new JumpTask
            {
                Title            = "设置目录",
                Description      = "打开应用存放设置的目录",
                ApplicationPath  = "explorer.exe",
                Arguments        = setting,
                IconResourcePath = "explorer.exe",
            };
        }

        public static JumpTask CreateNavigateToLogsTask()
        {
            var setting = Ioc.Get<IAppConfig>().DirOfLogs;

            return new JumpTask
            {
                Title            = "日志目录",
                Description      = "打开应用存放日志的目录",
                ApplicationPath  = "explorer.exe",
                Arguments        = setting,
                IconResourcePath = "explorer.exe",
            };
        }

        #endregion
    }
}
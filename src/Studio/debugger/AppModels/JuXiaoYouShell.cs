using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Workspaces;
using MaoTouGu.JuXiaoYou.Services.Plugins;
using MaoTouGu.Studio;
using MaoTouGu.Studio.References;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public class JuXiaoYouShell : ShellBase<MainWindow, StandaloneWindow>
    {
        bool IsConfigExists()
        {
            return true;
        }

        void RoutingToConfigPage()
        {

        }


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

        protected override void OnStartup()
        {
            // 
            // 此方法在调用前必须导航到一个VM，以避免因为没有VM程序直接退出。

            //
            // Find DataSource=

            //
            //
            var bsm = new BusyStateManager(FindDialogHost());

            bsm.Execute("注册插件", () =>
                                {
                                    FeatureManager.Scan();
                                    FeatureManager.Scan(DirectoryExt.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
                                    // ArcadiaEntryPoint.RegisterFeatures();
                                    // ArcadiaEntryPoint.RegisterVisualProvider();
                                })
               .Execute("加载数据....", async () => await InitializeGlobalServices())
               .Execute("引导", () =>
                              {
                              })
               .Execute("正在跳转...", () =>
                                   {
                                       GUI.RunOnUIThread(() =>
                                                         {
                                                             Navigate<CollectionTargetViewModel>();
                                                             ClosePlaceholdingView();
                                                         });
                                   })
               .Execute();

            // Navigate<NamingLevelSettingDetailsViewModel>();
            // RoutingToStartupPage();
            // Navigate<NamingLevelSettingDetailsViewModel>();
            // Navigate<ProfilingLevelSettingDetailsViewModel>();
            // Navigate<PaintingLevelSettingDetailsViewModel>();
            // Navigate<VisualLevelSettingDetailsViewModel>();
            // Navigate<GeometryPrototypingViewModel>();
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
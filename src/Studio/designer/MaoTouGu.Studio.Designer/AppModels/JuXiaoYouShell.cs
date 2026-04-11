using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Services.Imaging.Caching;
using MaoTouGu.JuXiaoYou.Workspaces;
using MaoTouGu.JuXiaoYou.Services.Plugins;
using MaoTouGu.Studio;
using MaoTouGu.Studio.References;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public class JuXiaoYouShell : ShellBase<MainWindow, StandaloneWindow>
    {
        protected sealed  override async void OnStart()
        {
            
            Ioc.Use<IImageCacheService, ImageCacheService>(new ImageCacheService()).Start();
            
            //
            //
            await Navigate<LauncherViewModel>();
            await Task.Delay(100)
                      .ContinueWith(x => GUI.RunOnUIThread(StartupImpl));
        }

        private DialogHost FindDialogHost()
        {
            return WindowTable.Values
                              .FirstOrDefault(x => x.IsActivate && x.DialogHost is not null)
                             ?.DialogHost;
        }

        void StartupImpl()
        {
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
                                                             Navigate(new DesignViewModel());
                                                             Navigate<LauncherViewModel>();
                                                             Close<LauncherViewModel>();
                                                         });
                                   })
               .Execute();
        }

        protected override void OnStartup()
        {
            // 
            // 此方法在调用前必须导航到一个VM，以避免因为没有VM程序直接退出。

            //
            // Find DataSource=

            
            


            // Navigate<NamingLevelSettingDetailsViewModel>();
            // RoutingToStartupPage();
            // Navigate<NamingLevelSettingDetailsViewModel>();
            // Navigate<ProfilingLevelSettingDetailsViewModel>();
            // Navigate<PaintingLevelSettingDetailsViewModel>();
            // Navigate<VisualLevelSettingDetailsViewModel>();
            // Navigate<GeometryPrototypingViewModel>();
        }
    }
}
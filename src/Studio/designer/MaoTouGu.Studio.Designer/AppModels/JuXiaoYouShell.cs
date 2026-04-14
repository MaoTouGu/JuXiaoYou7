using System.Windows;
using System.Windows.Media.Imaging;
using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Services.Imaging;
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
            // Initialize
            ImageSystem.Gravatar = new BitmapImage(new Uri(URI_Gravatar));
            ImageSystem.Icon     = new BitmapImage(new Uri(URI_Icon));
            ImageSystem.Image    = new BitmapImage(new Uri(URI_Image));
            
            //
            //
            await Navigate<LauncherViewModel>()
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
                                                             // Navigate(new DesignViewModel());
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

        public override async Task<bool> Navigate(PageBase page, params object[] args)
        {
            if (!await page.Receive(args))
            {
                return false;
            }
            
            if (!CanNavigateFixed(page, out var theSameOne))
            {
                if (InstanceTable.TryGetValue(theSameOne.GetHashCode(), out var ctx))
                {
                    var wnd = ctx.Window;

                    if (WindowTable.TryGetValue(wnd.GetHashCode(), out var ctx2))
                    {
                        ctx2.SetPage(theSameOne, false);
                    }

                    var last = wnd.WindowState;
                    wnd.WindowState = WindowState.Minimized;
                    wnd.Activate();
                    wnd.WindowState = last;
                }

                page.Dispose();
                return false;
            }
            
            GUI.RunOnUIThread(() =>
                              {
                                  MultipleWindowContext ctx;

                                  if (page is IHostedWindowNavigation)
                                  {
                                      ctx = FindMainWindowContentHost();

                                  }
                                  else
                                  {
                                      ctx = FindActivatedWindowContentHost();
                                  }

                                  if (ctx is null)
                                  {
                                      //
                                      // 等待窗口创建完成后自动完成导航。
                                      PendingQueue.Enqueue(page);
                
                                      //
                                      // 创建一个新的WindowContentHost
                                      var window = CreateNewWindowContentHost();
                                      window.Show();
                                  }
                                  else
                                  {
                                      
                                      ctx.SetPage(page);
                                      ctx.Tabs.Add(page);
                                  }

                              });

            return true;
        }
    }
}
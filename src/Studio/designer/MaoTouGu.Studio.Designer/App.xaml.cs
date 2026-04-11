using System.Configuration;
using System.Data;
using System.Windows;
using MaoTouGu.Foundation;
using MaoTouGu.Shells.AppConfigs;
using MaoTouGu.Shells.AppModels;
using MaoTouGu.Shells.Languages;
using System.Windows.Shell;
using MaoTouGu.Foundation.Collections;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Shells;
using MaoTouGu.Shells.Generators;

namespace MaoTouGu.JuXiaoYou
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MTGApplication
    {
        protected override IAppConfig BuildAppHost(IAppConfigBuilder builder)
        {
            AppDir.Initialize("JuXiaoYou");
            return builder.UseLogsDir(DirectoryExt.Combine(AppDir.App, "Logs"))
                          .UseSettingDir(DirectoryExt.Combine(AppDir.App, "Settings"))
                          .UseSetting<AppSettings>(AppSettings.FromFile)
                          .UseLanguageOptions((setting, provider) =>
                                              {
                                                  provider.SetLCID(setting.LCID);
                                                  provider.UseShellText();
                                                  provider.UseJuXiaoYouText();
                                                  // provider.UseArcadia();
                                              })
                          .UseTheme(x => (AppTheme)x.Theme)
                          .UseViews(new[]
                           {
                               new AppBundleStateProvider(),
                               new CoreBundleStateProvider(),
                               // new ArcadiaBundleStateProvider(),
                               Dialog.UseBuiltinViews(),
                               GravatarSystem.UseBuiltinViews(),
                           })
                          .Build(new JuXiaoYouShell());

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //
            // 应用主题
            ApplyTheme();

            //
            // 创建跳转列表
            CreateJumpList();

            //
            //
            GlobalSettings.Load();

            //
            //
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void ApplyTheme()
        {

            string source;
            string dockSource;

            if (IsCustomTheme)
            {
                source     = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Light.xaml";
                dockSource = "pack://application:,,,/AvalonDock.Themes.VS2013;component/LightTheme.xaml";
            }
            else if (IsDarkTheme)
            {
                source     = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Dark.xaml";
                dockSource = "pack://application:,,,/AvalonDock.Themes.VS2013;component/DarkTheme.xaml";
            }
            else
            {
                source     = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Light.xaml";
                dockSource = "pack://application:,,,/AvalonDock.Themes.VS2013;component/LightTheme.xaml";
            }

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(source) });
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(dockSource) });
        }

        private void CreateJumpList()
        {
            // 创建 JumpList
            var jumpList = new JumpList();

            JumpList.SetJumpList(this, jumpList);

            jumpList.JumpItems.Add(JuXiaoYouShell.CreateNavigateToSettingTask());
            jumpList.JumpItems.Add(JuXiaoYouShell.CreateNavigateToLogsTask());

            // 刷新 JumpList
            jumpList.Apply();
        }
    }
}
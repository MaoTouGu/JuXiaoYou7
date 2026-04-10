using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Shell;
using MaoTouGu.Foundation;
using MaoTouGu.JuXiaoYou;
using MaoTouGu.JuXiaoYou.AppModels;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Shells.AppConfigs;
using MaoTouGu.Shells.Generators;
using MaoTouGu.Shells.Languages;

namespace MaoTouGu.Teams
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
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
                                              })
                          .UseTheme(x => (AppTheme)x.Theme)
                          .UseViews(new[]
                           {
                               new AppBundleStateProvider(),
                               new CoreBundleStateProvider(),
                               new IMBundleStateProvider(),
                               Dialog.UseBuiltinViews(),
                               GravatarSystem.UseBuiltinViews(),
                           })
                          .Build(new ChatShell());

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

            if (IsCustomTheme)
            {
                source = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Light.xaml";
            }
            else if (IsDarkTheme)
            {
                source = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Dark.xaml";
            }
            else
            {
                source = "pack://application:,,,/KinonekoSoftware.UI.OnWPF;component/Themes/Light.xaml";
            }

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(source) });
        }

        private void CreateJumpList()
        {
            // 创建 JumpList
            var jumpList = new JumpList();

            JumpList.SetJumpList(this, jumpList);

            jumpList.JumpItems.Add(ChatShell.CreateNavigateToSettingTask());
            jumpList.JumpItems.Add(ChatShell.CreateNavigateToLogsTask());

            // 刷新 JumpList
            jumpList.Apply();
        }
    }
}
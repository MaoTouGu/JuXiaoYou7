using System.Configuration;
using System.Data;
using System.Windows;
using MaoTouGu.Foundation;
using MaoTouGu.Shells.AppConfigs;
using MaoTouGu.Shells.AppModels;
using MaoTouGu.Shells.Base;
using MaoTouGu.Shells.Core;
using MaoTouGu.Shells.Runer.AppModels;

namespace MaoTouGu.Shells.Runer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App() : base()
        {

        }



        protected override IAppConfig BuildAppHost(IAppConfigBuilder builder)
        {
            return builder.UseDefaultDir()
                          .UseSetting<AppSetting>(x => x.FromFile("setting.json", ()=> new AppSetting()))
                          .UseLanguageOptions((setting, provider) =>
                                              {
                                                  provider.SetLCID(setting.LCID);
                                                  provider.UseFile("test.i18n");
                                              })
                          .UseTheme(x => x.Theme)
                          .UseViews(new[] { typeof(App).Assembly })
                          .UseViews(new []
                           {
                               Dialog.UseBuiltinViews(),
                           })
                          .Build(new SpaModel());

        }
    }
}
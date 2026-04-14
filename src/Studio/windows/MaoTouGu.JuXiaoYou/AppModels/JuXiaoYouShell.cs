using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Internals;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Workspaces;
using MaoTouGu.JuXiaoYou.Prototypings;
using MaoTouGu.JuXiaoYou.Services.Plugins;
using MaoTouGu.Studio;
using MaoTouGu.Studio.References;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public class JuXiaoYouShell : ShellBase<MainWindow, StandaloneWindow>
    {
        // bool IsConfigExists()
        // {
        //     return true;
        // }
        //
        // void RoutingToConfigPage()
        // {
        //
        // }
        //
        // public void RoutingToStartupPage()
        // {
        //     switch (GlobalSettings.AppSettings.StartupRouting)
        //     {
        //         case StartupRouting.Home:
        //             Navigate<HomeViewModel>();
        //             break;
        //         case StartupRouting.Project:
        //             Navigate<ProjectViewModel>();
        //             break;
        //         case StartupRouting.Prototype:
        //             Navigate<PrototypingViewModel>();
        //             break;
        //         case StartupRouting.Inspiration:
        //             Navigate<InspirationViewModel>();
        //             break;
        //         case StartupRouting.SliceNarrative:
        //             Navigate<SliceNarrativeViewModel>();
        //             break;
        //         case StartupRouting.ConversationNarrative:
        //             Navigate<ConversationNarrativeViewModel>();
        //             break;
        //         case StartupRouting.Outline:
        //             Navigate<OutlineViewModel>();
        //             break;
        //         case StartupRouting.Texting:
        //             Navigate<TextingViewModel>();
        //             break;
        //
        //     }
        // }

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
                                                             Navigate<WorkspaceViewModel>();
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
    }
}
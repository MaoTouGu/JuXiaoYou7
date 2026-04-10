// ----------------------------------------------------------
//            文件：GlobalSettings.Flyout.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou
{
    //
    // 该文件分为多个部分，
    // GlobalSettings类，用于实现与Flyout相关的代码逻辑，包括加载Flyout设置、保存Flyout设置。
    //
    // FlyoutSettings类，用于描述Flyout设置的POCO类定义
    //
    // FlyoutSettingManager类，用于封装与某个页面是否需要Flyout弹窗引导相关的逻辑
    //
    // ShellBase<TMainWindow, THostWindow>部分用于实现默认的Flyout弹窗操作。

    partial class GlobalSettings
    {
        public const string FileName_Flyout = "JuXiaoYou-V7-Flyout.Json";

        /// <summary>
        /// 加载设置。
        /// </summary>
        public static void LoadFlyoutSettings()
        {
            FlyoutSettings = JSON.FromFile<FlyoutSettings>(FileNameOfFlyoutSettings, () => new FlyoutSettings
            {
                Table = new HashSet<string>(),
            });

            FlyoutSettingManager = new FlyoutSettingManager(FlyoutSettings.Table);
        }

        private static void SaveFlyoutSettingsImpl()
        {
            FlyoutSettingManager.Save();
            JSON.ToFile(FileNameOfFlyoutSettings, FlyoutSettings);
        }

        public static void SaveFlyoutSettings(bool forceSave = false)
        {
            if (forceSave)
            {
                SaveFlyoutSettingsImpl();
            }

            if (FlyoutSettingManager.Changes <= 0)
            {
                return;
            }

            SaveFlyoutSettingsImpl();

        }



        public static FlyoutSettings       FlyoutSettings       { get; set; }
        public static FlyoutSettingManager FlyoutSettingManager { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class FlyoutSettings
    {
        public HashSet<string> Table { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="table"></param>
    public sealed class FlyoutSettingManager(HashSet<string> table)
    {

        public bool Get(ViewModelBase vm)
        {
            var name = vm.GetType().Name;
            return table.Contains(name);
        }

        public void Save()
        {
            Changes = 0;
        }

        public void Set(ViewModelBase vm)
        {
            var name = vm.GetType().Name;

            if (table.Add(name))
            {
                Changes += 1;
            }
        }

        public int Changes { get; private set; }
    }



}

//
//
namespace MaoTouGu.JuXiaoYou.AppModels
{

    //
    //ShellBase关于Flyout部分的定义。
    partial class ShellBase<TMainWindow, THostWindow>
    {
        public sealed override void WhenFlyout(ViewModelBase vm)
        {
            GlobalSettings.FlyoutSettingManager?.Set(vm);

            WhenFlyoutOverride(vm);
        }

        protected virtual void WhenFlyoutOverride(ViewModelBase target)
        {

        }

        public sealed override bool ShouldFlyout(ViewModelBase target)
        {
            var fsm = GlobalSettings.FlyoutSettingManager;

            if (fsm is null)
            {
                return false;
            }

            return !fsm.Get(target);
        }

    }
}
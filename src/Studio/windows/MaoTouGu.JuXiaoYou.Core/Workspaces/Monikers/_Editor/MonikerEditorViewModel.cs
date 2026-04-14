// ----------------------------------------------------------
//            文件：MonikerEditorViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 00:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class MonikerEditorViewModel : InstancePage
    {
        public MonikerEditorViewModel(Moniker moniker)
        {
            Title      = $"编辑：{moniker.FriendlyName}";
            InstanceID = moniker.Id;
            Moniker    = moniker;
        }

        public MonikerSettingSet Settings => Moniker.Settings;

        private MonikerSetting _setting;

        public MonikerSetting Setting
        {
            get => _setting;
            set => SetValue(ref _setting, value);
        }

        public Moniker Moniker { get; }
    }
}
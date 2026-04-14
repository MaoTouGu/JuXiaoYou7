// ----------------------------------------------------------
//            文件：VisualizerControl.DataChanged.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月13日 14:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Controls
{
    partial class VisualizerControl
    {
        
        /// <summary>
        /// 给定一个Moniker和一个喵喵咒语，获得绑定。
        /// </summary>
        /// <param name="moniker"></param>
        /// <param name="setting"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        protected Binding GetBinding(Moniker moniker, string setting, IValueConverter converter = null)
        {
            if (!moniker.ContainSettingItem(setting) && !string.IsNullOrEmpty(setting))
            {
                moniker.Settings.Add(setting, string.Empty);
            }

            var binding = new Binding
            {
                Source    = moniker.Settings,
                Path      = new PropertyPath($"[{setting}]"),
                Mode      = BindingMode.OneWay,
                Converter = converter,
            };

            return binding;
        }
        
        /// <summary>
        /// 给定一个IVisualizerOptions，获得绑定。
        /// </summary>
        /// <param name="options"></param>
        /// <param name="setting"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        protected Binding GetBinding(IVisualizerOptions options, string setting, IValueConverter converter = null)
        {
            var binding = new Binding
            {
                Source    = options,
                Path      = new PropertyPath(setting),
                Mode      = BindingMode.OneWay,
                Converter = converter,
            };

            return binding;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetMonitor()
        {
            Interaction.GetBehaviors(this)
                       .Add(new MonikerSettingMonitor());
        }
    }
}
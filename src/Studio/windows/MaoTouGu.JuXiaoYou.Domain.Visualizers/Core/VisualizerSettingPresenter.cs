// ----------------------------------------------------------
//            文件：VisualizerSettingPresenter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 21:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows.Markup;
using MaoTouGu.Studio.Controls;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public class VisualizerSettingPresenter  : ContentPresenter, IAddChild
    {
        void IAddChild.AddChild(object value)
        {
            if (value is FrameworkElement fe)
            {
                Content = fe;
            }
        }
        
        void IAddChild.AddText(string text)
        {

        }

        public VisualizerSettingPresenter()
        {
            DataContextChanged += OnDataContextChanged;
        }
        
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TypographyVisualizerVPO value)
            {
                var _visualizer = value.Instance;
                var feature     = FeatureManager.Visualizers.SafetyGet(_visualizer.Visualizer);

                if (feature?.SettingType is null)
                {
                    return;
                }

                var options = feature.CreateOptions(_visualizer.Base64);

                if (Activator.CreateInstance(feature.SettingType) is not VisualizerControl view)
                {
                    return;
                }
                
                view.Moniker = value.Moniker;
                view.Options = options;
                Content      = view;
            }
        }
    }
}
// ----------------------------------------------------------
//            文件：VisualizerViewPresenter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 21:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows.Markup;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    [Obsolete("使用Visualizer的转化器来代替")]
    public sealed class VisualizerViewPresenter : ContentPresenter, IAddChild
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

        public VisualizerViewPresenter()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TypographyVisualizerVPO value)
            {
                var _visualizer = value.Instance;
                var feature     = FeatureManager.Visualizers.SafetyGet(_visualizer.Visualizer);

                if (feature?.ViewType is null)
                {
                    return;
                }

                var options = feature.CreateOptions(_visualizer.Base64);

                if (Activator.CreateInstance(feature.ViewType) is not VisualizerControl view)
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
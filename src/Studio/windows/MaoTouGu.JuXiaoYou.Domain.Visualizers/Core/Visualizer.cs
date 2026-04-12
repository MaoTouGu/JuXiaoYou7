// ----------------------------------------------------------
//            文件：Visualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 21:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public static class Visualizer
    {
        static FrameworkElement Create(TypographyVisualizerVPO value, Func<IVisualizerGenerator, Type> expression)
        {
            var _visualizer = value.Instance;
            var feature     = FeatureManager.Visualizers.SafetyGet(_visualizer.Visualizer);

            if (feature?.SettingType is null)
            {
                return null;
            }

            var options = value.Options ?? feature.CreateOptions(_visualizer.Base64);

            if (Activator.CreateInstance(expression(feature)) is not VisualizerControl view)
            {
                return null;
            }

            value.Options = options;
            
            view.SetBinding(VisualizerControl.MonikerProperty, new Binding
            {
                Source = value,
                Path = new PropertyPath(nameof(TypographyVisualizerVPO.Moniker)),
                Mode = BindingMode.OneWay,
            });
            
            view.SetBinding(VisualizerControl.OptionsProperty, new Binding
            {
                Source = value,
                Path = new PropertyPath(nameof(TypographyVisualizerVPO.Options)),
                Mode = BindingMode.OneWay,
            });
            
            return view;
        }
        
        
        static FrameworkElement Create(TypographyVisualizerVPO value)
        {
            var _visualizer = value.Instance;
            var feature     = FeatureManager.Visualizers.SafetyGet(_visualizer.Visualizer);

            if (feature?.SettingType is null)
            {
                return null;
            }

            var options = value.Options ?? feature.CreateOptions(_visualizer.Base64);

            if (Activator.CreateInstance(feature.SettingType) is not UserControl view)
            {
                return null;
            }

            view.DataContext = options;
            
            view.SetBinding(FrameworkElement.DataContextProperty, new Binding
            {
                Source = value,
                Path   = new PropertyPath(nameof(TypographyVisualizerVPO.Options)),
                Mode   = BindingMode.OneWay,
            });

            return view;
        }
        
        sealed class SettingCreator : OneWayConverter
        {

            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is TypographyVisualizerVPO v)
                {
                    return Create(v);
                }

                return null;
            }
        }
        
        sealed class ViewCreator : OneWayConverter
        {

            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is TypographyVisualizerVPO v)
                {
                    return Create(v, x => x.ViewType);
                }

                return null;
            }
        }

        public static readonly IValueConverter Setting = new SettingCreator();
        public static readonly IValueConverter View    = new ViewCreator();
    }
}
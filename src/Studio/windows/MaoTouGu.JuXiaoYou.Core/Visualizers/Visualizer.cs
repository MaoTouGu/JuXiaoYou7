// ----------------------------------------------------------
//            文件：Visualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 21:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using MaoTouGu.JuXiaoYou.Visualizers.Commons;
using CornerRadiusConverter = MaoTouGu.JuXiaoYou.Visualizers.Commons.CornerRadiusConverter;
using FontFamilyConverter = MaoTouGu.JuXiaoYou.Visualizers.Commons.FontFamilyConverter;
using FontStyleConverter = MaoTouGu.JuXiaoYou.Visualizers.Commons.FontStyleConverter;
using FontWeightConverter = MaoTouGu.JuXiaoYou.Visualizers.Commons.FontWeightConverter;
using ThicknessConverter = MaoTouGu.JuXiaoYou.Visualizers.Commons.ThicknessConverter;

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
                Path   = new PropertyPath(nameof(TypographyVisualizerVPO.Moniker)),
                Mode   = BindingMode.OneWay,
            });

            view.SetBinding(VisualizerControl.OptionsProperty, new Binding
            {
                Source = value,
                Path   = new PropertyPath(nameof(TypographyVisualizerVPO.Options)),
                Mode   = BindingMode.OneWay,
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

                if (value is TypographyTextVPO)
                {
                    return new TypographyTextSettingView { DataContext = value };
                }

                if (value is TypographyImageVPO)
                {
                    return new TypographyImageSettingView { DataContext = value };
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


        public static readonly CornerRadiusConverter        CornerRadius        = new CornerRadiusConverter();
        public static readonly ThicknessConverter           Thickness           = new ThicknessConverter();
        public static readonly FontWeightConverter          FontWeight          = new FontWeightConverter();
        public static readonly FontFamilyConverter          FontFamily          = new FontFamilyConverter();
        public static readonly FontStyleConverter           FontStyle           = new FontStyleConverter();
        public static readonly HorizontalAlignmentConverter HorizontalAlignment = new HorizontalAlignmentConverter();
        public static readonly VerticalAlignmentConverter   VerticalAlignment   = new VerticalAlignmentConverter();
        public static readonly TextAlignmentConverter       TextAlignment       = new TextAlignmentConverter();
    }
}
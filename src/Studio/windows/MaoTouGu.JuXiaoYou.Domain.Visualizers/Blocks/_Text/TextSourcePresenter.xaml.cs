// ----------------------------------------------------------
//            文件：TextSourcePresenter.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public partial class TextSourcePresenter
    {
        public TextSourcePresenter()
        {
            InitializeComponent();
        }

        protected override void Setup(Moniker m, IVisualizerOptions options)
        {
            if (options is not TextSourceVisualizer tsv)
            {
                return;
            }

            if (string.IsNullOrEmpty(tsv.MetadataSource))
            {
                BindingOperations.ClearBinding(TextBlock, TextBlock.TextProperty);
            }
            else
            {
                TextBlock.SetBinding(TextBlock.TextProperty, GetBinding(m, tsv.MetadataSource));
            }
        }

        T Get<T, E>(IValueConverter converter, E value)
        {
            return (T)converter.Convert(value, null, null, CultureInfo.CurrentUICulture);
        }

        protected override void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not TextSourceVisualizer tsv)
            {
                return;
            }

            Border.Background      = Xaml.ToBrush(tsv.Background);
            Border.BorderBrush     = Xaml.ToBrush(tsv.BorderBrush);
            Border.BorderThickness = Get<Thickness, Int32Thickness>(Visualizer.Thickness, tsv.BorderThickness);
            Border.CornerRadius    = Get<CornerRadius, Int32CornerRadius>(Visualizer.CornerRadius, tsv.CornerRadius);
            Border.Padding         = Get<Thickness, Int32Thickness>(Visualizer.Thickness, tsv.Padding);

            //
            //
            TextBlock.Foreground          = Xaml.ToBrush(tsv.Foreground);
            TextBlock.FontStyle           = Get<FontStyle, bool>(Visualizer.FontStyle, tsv.IsBold);
            TextBlock.FontWeight          = Get<FontWeight, int>(Visualizer.FontWeight, tsv.FontWeight);
            TextBlock.FontFamily          = Get<FontFamily, string>(Visualizer.FontFamily, tsv.FontFamily);
            TextBlock.FontSize            = tsv.FontSize;
            TextBlock.HorizontalAlignment = Get<HorizontalAlignment, int>(Visualizer.HorizontalAlignment, tsv.HorizontalAlignment);
            TextBlock.VerticalAlignment   = Get<VerticalAlignment, int>(Visualizer.VerticalAlignment, tsv.VerticalAlignment);


            if (string.IsNullOrEmpty(tsv.MetadataSource))
            {
                BindingOperations.ClearBinding(TextBlock, TextBlock.TextProperty);
            }
            else
            {
                TextBlock.SetBinding(TextBlock.TextProperty, GetBinding(m, tsv.MetadataSource));
            }
        }
    }
}
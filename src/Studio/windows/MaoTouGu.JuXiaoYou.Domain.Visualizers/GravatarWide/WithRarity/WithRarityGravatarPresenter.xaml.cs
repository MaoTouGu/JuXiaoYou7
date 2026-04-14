// ----------------------------------------------------------
//            文件：WithRarityGravatarPresenter.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.GravatarWide
{
    public partial class WithRarityGravatarPresenter
    {
        public WithRarityGravatarPresenter()
        {
            InitializeComponent();
        }

        protected override void Setup(Moniker m, IVisualizerOptions options)
        {
            OptionChangedOverride(m, options);
        }


        protected override void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not WithRarityGravatarVisualizer visualizer)
            {
                return;
            }


            var brush    = new LinearGradientBrush();
            var stop1    = new GradientStop();
            var stop2    = new GradientStop(Colors.Transparent, 0.9);
            var binding  = GetBinding(m, visualizer.MetadataSource, Converters.ToColor);
            var binding2 = GetBinding(m, nameof(Moniker.Name));
            var binding3 = GetBinding(m, nameof(Moniker.Gravatar));

            brush.GradientStops.Add(stop1);
            brush.GradientStops.Add(stop2);
            brush.StartPoint = new Point();
            brush.EndPoint   = new Point(0, 1);

            BindingOperations.SetBinding(stop1, GradientStop.ColorProperty, binding);
            
            //
            //
            Name.SetBinding(TextBlock.TextProperty, binding2);
            Border.SetBinding(ImageSystem.SourceProperty, binding3);

            //
            //
            Background = brush;
        }
    }
}
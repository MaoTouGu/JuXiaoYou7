// ----------------------------------------------------------
//            文件：RectanglePresenter.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 22:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public partial class RectanglePresenter
    {
        public RectanglePresenter()
        {
            InitializeComponent();
        }

        protected override void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not RectangleVisualizer visualizer)
            {
                return;
            }

            
            if (string.IsNullOrEmpty(visualizer.MetadataSource))
            {
                Background = null;
            }
            else
            {
                var brush   = new SolidColorBrush();
                var binding = GetBinding(m, visualizer.MetadataSource, Converters.ToColor);


                BindingOperations.SetBinding(brush, SolidColorBrush.ColorProperty, binding);
                Background = brush;
            }

        }
    }
}
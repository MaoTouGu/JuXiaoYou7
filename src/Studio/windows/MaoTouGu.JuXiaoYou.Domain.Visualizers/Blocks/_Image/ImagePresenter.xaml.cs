// ----------------------------------------------------------
//            文件：ImagePresenter.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 23:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public partial class ImagePresenter
    {
        public ImagePresenter()
        {
            InitializeComponent();
        }

        protected override void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not ImageVisualizer visualizer)
            {
                return;
            }

            var brush   = new SolidColorBrush();
            var binding = GetBinding(m, visualizer.MetadataSource);

            
            Image.SetBinding(ImageSystem.SourceProperty, binding);
            Background = brush;
        }
    }
}
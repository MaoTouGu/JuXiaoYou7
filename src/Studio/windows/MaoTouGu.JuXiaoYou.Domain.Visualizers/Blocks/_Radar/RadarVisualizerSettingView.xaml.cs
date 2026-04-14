// ----------------------------------------------------------
//            文件：RadarVisualizerSettingView.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 16:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public partial class RadarVisualizerSettingView : UserControl
    {
        public RadarVisualizerSettingView()
        {
            InitializeComponent();
        }
        
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            if (DataContext is RadarVisualizer rv && 
                sender is FrameworkElement{ DataContext: RadarItemFrom item})
            {
                rv.Edit.Execute(item);
            }
        }
    }
}
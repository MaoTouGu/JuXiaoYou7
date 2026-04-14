// ----------------------------------------------------------
//            文件：FontStyleEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class FontStyleEditorItem : UserControl
    {
        public FontStyleEditorItem()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ITextTarget target)
            {
                Input.IsChecked = target.IsBold;
            }
        }


        private void Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox && DataContext is ITextTarget target)
            {
                target.IsBold = true;
            }
        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox && DataContext is ITextTarget target)
            {
                target.IsBold = false;
            }
        }
    }
}
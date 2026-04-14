// ----------------------------------------------------------
//            文件：VerticalAlignmentEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class VerticalAlignmentEditorItem : UserControl
    {
        public VerticalAlignmentEditorItem()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ITextTarget target)
            {
                for (var i = 0; i < Input.Items.Count; i++)
                {
                    if ((Input.Items[i] as FrameworkElement)?.Tag is int n &&
                        n == target.VerticalAlignment)
                    {
                        Input.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox l                          &&
                l.SelectedItem is ListBoxItem { Tag: int n } &&
                DataContext is ITextTarget target)
            {
                target.VerticalAlignment = n;
            }
        }
    }
}
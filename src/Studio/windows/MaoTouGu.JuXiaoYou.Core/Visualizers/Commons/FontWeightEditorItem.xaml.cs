// ----------------------------------------------------------
//            文件：FontWeightEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class FontWeightEditorItem : UserControl
    {
        public FontWeightEditorItem()
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
                        n == target.FontWeight)
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
                target.FontWeight = n;
            }
        }
    }
}
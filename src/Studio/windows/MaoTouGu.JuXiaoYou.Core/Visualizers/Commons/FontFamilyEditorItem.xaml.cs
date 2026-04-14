// ----------------------------------------------------------
//            文件：FontFamilyEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class FontFamilyEditorItem : UserControl
    {
        public FontFamilyEditorItem()
        {
            InitializeComponent();
            
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            foreach (var typeface in Fonts.SystemFontFamilies)
            {
                FontFamilies.Items.Add(typeface);
            }
        }
        
        private void FontFamilies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ITextTarget target && FontFamilies.SelectedItem is FontFamily ff)
            {
                target.FontFamily = ff.Source;
            }
        }
    }
}
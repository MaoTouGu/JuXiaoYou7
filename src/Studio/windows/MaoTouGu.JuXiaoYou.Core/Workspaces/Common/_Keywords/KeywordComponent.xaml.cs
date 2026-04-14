// ----------------------------------------------------------
//            文件：KeywordComponent.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Common
{
    public partial class KeywordComponent : UserControl
    {
        public KeywordComponent()
        {
            InitializeComponent();
        }

        private async void Button_Add(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement { DataContext: Keyword k } fe)
            {
                return;
            }

            var parent = Xaml.FindVisualParent<Selector>(fe);

            if (parent.DataContext is not IKeywordTarget iKT)
            {
                return;
            }

            await iKT.AddKeyword();
        }

        private async void Button_Remove(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement { DataContext: Keyword k } fe)
            {
                return;
            }

            var parent = Xaml.FindVisualParent<Selector>(fe);

            if (parent.DataContext is not IKeywordTarget iKT)
            {
                return;
            }

            await iKT.RemoveKeyword(k);
        }
    }
}
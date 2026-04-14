// ----------------------------------------------------------
//            文件：TypographyImageView.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 11:50
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class TypographyImageView : UserControl
    {
        public TypographyImageView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var page = Xaml.FindVisualParent<ForestPage>(this);


            if (page is not ForestPage { DataContext: IValueConverter converter })
            {
                return;
            }

            Image.SetBinding(Image.SourceProperty, new Binding
            {
                Source    = DataContext,
                Converter = converter,
                Path      = new PropertyPath(nameof(TypographyImageVPO.Source)),
                Mode      = BindingMode.OneWay,
            });
        }
    }
}
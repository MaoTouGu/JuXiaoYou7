// ----------------------------------------------------------
//            文件：ThicknessEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:48
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class ThicknessEditorItem : UserControl
    {
        public ThicknessEditorItem()
        {
            InitializeComponent();

            //
            //
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not ITextTarget target)
            {
                return;

            }

            Left.Text   = target.Padding.Left.ToString();
            Right.Text  = target.Padding.Right.ToString();
            Top.Text    = target.Padding.Top.ToString();
            Bottom.Text = target.Padding.Bottom.ToString();
        }

        private void Left_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target.Padding = c;
                target.RaisePropertyChanged(nameof(ITextTarget.Padding));
            }
        }

        private void Right_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target.Padding = c;
                target.RaisePropertyChanged(nameof(ITextTarget.Padding));
            }
        }

        private void Top_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target.Padding = c;
                target.RaisePropertyChanged(nameof(ITextTarget.Padding));
            }
        }

        private void Bottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target.Padding = c;
                target.RaisePropertyChanged(nameof(ITextTarget.Padding));
            }
        }
    }
}
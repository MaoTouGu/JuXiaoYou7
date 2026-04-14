// ----------------------------------------------------------
//            文件：BorderThicknessEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 16:49
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class BorderThicknessEditorItem : UserControl
    {
        public BorderThicknessEditorItem()
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

            Left.Text   = target.BorderThickness.Left.ToString();
            Right.Text  = target.BorderThickness.Right.ToString();
            Top.Text    = target.BorderThickness.Top.ToString();
            Bottom.Text = target.BorderThickness.Bottom.ToString();
        }

        private void Left_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                target.RaisePropertyChanged(nameof(ITextTarget.BorderThickness));
            }
        }

        private void Right_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                target.RaisePropertyChanged(nameof(ITextTarget.BorderThickness));
            }
        }

        private void Top_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                target.RaisePropertyChanged(nameof(ITextTarget.BorderThickness));
            }
        }

        private void Bottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                target.RaisePropertyChanged(nameof(ITextTarget.BorderThickness));
            }
        }
    }
}
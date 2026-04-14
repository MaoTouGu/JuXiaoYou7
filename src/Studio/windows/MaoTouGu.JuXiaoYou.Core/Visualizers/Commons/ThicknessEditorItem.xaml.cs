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
            if (e.NewValue is ITextTarget target)
            {
                Left.Text   = target.Padding.Left.ToString();
                Right.Text  = target.Padding.Right.ToString();
                Top.Text    = target.Padding.Top.ToString();
                Bottom.Text = target.Padding.Bottom.ToString();
            }
            else if (e.NewValue is IPaddingTarget target2)
            {
                Left.Text   = target2.Padding.Left.ToString();
                Right.Text  = target2.Padding.Right.ToString();
                Top.Text    = target2.Padding.Top.ToString();
                Bottom.Text = target2.Padding.Bottom.ToString();
            }
        }


        void Update()
        {
            (DataContext as INotifyPropertyChangedEX)?.RaisePropertyChanged(nameof(ITextTarget.Padding));
        }

        private void Left_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target.Padding = c;
                Update();
            }
            else if (DataContext is IPaddingTarget target2)
            {
                
                var c = target2.Padding;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target2.Padding = c;
                Update();
            }
        }

        private void Right_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target.Padding = c;
                Update();
            }
            else if (DataContext is IPaddingTarget target2)
            {
                
                var c = target2.Padding;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target2.Padding = c;
                Update();
            }
        }

        private void Top_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target.Padding = c;
                Update();
            }
            else if (DataContext is IPaddingTarget target2)
            {
                
                var c = target2.Padding;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target2.Padding = c;
                Update();
            }
        }

        private void Bottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.Padding;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target.Padding = c;
                Update();
            }
            else if (DataContext is IPaddingTarget target2)
            {
                
                var c = target2.Padding;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target2.Padding = c;
                Update();
            }
        }
    }
}
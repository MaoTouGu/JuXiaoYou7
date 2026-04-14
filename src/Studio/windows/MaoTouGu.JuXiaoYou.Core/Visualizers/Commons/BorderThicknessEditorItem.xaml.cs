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
            if (e.NewValue is ITextTarget target)
            {

                Left.Text   = target.BorderThickness.Left.ToString();
                Right.Text  = target.BorderThickness.Right.ToString();
                Top.Text    = target.BorderThickness.Top.ToString();
                Bottom.Text = target.BorderThickness.Bottom.ToString();

            }
            else if (e.NewValue is IBorderThicknessTarget target2)
            {
                Left.Text   = target2.BorderThickness.Left.ToString();
                Right.Text  = target2.BorderThickness.Right.ToString();
                Top.Text    = target2.BorderThickness.Top.ToString();
                Bottom.Text = target2.BorderThickness.Bottom.ToString();
            }
        }

        void Update()
        {
            (DataContext as INotifyPropertyChangedEX)?.RaisePropertyChanged(nameof(ITextTarget.BorderThickness));
        }

        private void Left_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                Update();
            }
            else if (DataContext is IBorderThicknessTarget target2)
            {
                
                var c = target2.BorderThickness;
                c.Left = int.TryParse(Left.Text, out var b) ? b : 0;

                target2.BorderThickness = c;
                Update();
            }
        }

        private void Right_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                Update();
            }
            else if (DataContext is IBorderThicknessTarget target2)
            {
                
                var c = target2.BorderThickness;
                c.Right = int.TryParse(Right.Text, out var b) ? b : 0;

                target2.BorderThickness = c;
                Update();
            }
        }

        private void Top_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                Update();
            }
            else if (DataContext is IBorderThicknessTarget target2)
            {
                
                var c = target2.BorderThickness;
                c.Top = int.TryParse(Top.Text, out var b) ? b : 0;

                target2.BorderThickness = c;
                Update();
            }
        }

        private void Bottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.BorderThickness;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target.BorderThickness = c;
                Update();
            }
            else if (DataContext is IBorderThicknessTarget target2)
            {
                
                var c = target2.BorderThickness;
                c.Bottom = int.TryParse(Bottom.Text, out var b) ? b : 0;

                target2.BorderThickness = c;
                Update();
            }
        }
    }
}
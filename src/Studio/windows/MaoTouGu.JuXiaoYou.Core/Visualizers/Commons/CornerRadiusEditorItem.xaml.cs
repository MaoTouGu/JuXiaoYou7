// ----------------------------------------------------------
//            文件：CornerRadiusEditorItem.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public partial class CornerRadiusEditorItem : UserControl
    {
        public CornerRadiusEditorItem()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        void Update()
        {
            (DataContext as INotifyPropertyChangedEX)?.RaisePropertyChanged(nameof(ICornerRadiusTarget.CornerRadius));
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ITextTarget target)
            {
                LeftTop.Text     = target.CornerRadius.LeftTop.ToString();
                RightTop.Text    = target.CornerRadius.RightTop.ToString();
                RightBottom.Text = target.CornerRadius.RightBottom.ToString();
                LeftBottom.Text  = target.CornerRadius.LeftBottom.ToString();
            }
            else if (e.NewValue is ICornerRadiusTarget target2)
            {
                LeftTop.Text     = target2.CornerRadius.LeftTop.ToString();
                RightTop.Text    = target2.CornerRadius.RightTop.ToString();
                RightBottom.Text = target2.CornerRadius.RightBottom.ToString();
                LeftBottom.Text  = target2.CornerRadius.LeftBottom.ToString();
            }
        }

        private void LeftTop_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.CornerRadius;
                c.LeftTop = int.TryParse(LeftTop.Text, out var b) ? b : 0;

                target.CornerRadius = c;
                Update();
            }
            else if (DataContext is ICornerRadiusTarget target2)
            {
                
                var c = target2.CornerRadius;
                c.LeftTop = int.TryParse(LeftTop.Text, out var b) ? b : 0;

                target2.CornerRadius = c;
                Update();
            }
        }

        private void RightTop_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.CornerRadius;
                c.RightTop = int.TryParse(RightTop.Text, out var b) ? b : 0;

                target.CornerRadius = c;
                Update();
            }
            else if (DataContext is ICornerRadiusTarget target2)
            {
                
                var c = target2.CornerRadius;
                c.RightTop = int.TryParse(RightTop.Text, out var b) ? b : 0;

                target2.CornerRadius = c;
                Update();
            }
        }

        private void RightBottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.CornerRadius;
                c.RightBottom = int.TryParse(RightBottom.Text, out var b) ? b : 0;

                target.CornerRadius = c;
                Update();
            }
            else if (DataContext is ICornerRadiusTarget target2)
            {
                
                var c = target2.CornerRadius;
                c.RightBottom = int.TryParse(RightBottom.Text, out var b) ? b : 0;

                target2.CornerRadius = c;
                Update();
            }
        }

        private void LeftBottom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is ITextTarget target)
            {
                var c = target.CornerRadius;
                c.LeftBottom = int.TryParse(LeftBottom.Text, out var b) ? b : 0;

                target.CornerRadius = c;
                Update();
            }
            else if (DataContext is ICornerRadiusTarget target2)
            {
                
                var c = target2.CornerRadius;
                c.LeftBottom = int.TryParse(LeftBottom.Text, out var b) ? b : 0;

                target2.CornerRadius = c;
                Update();
            }
        }
    }
}
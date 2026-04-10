using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using KinonekoSoftware.UI;
using MaoTouGu.Shells.Behaviors;
using Brushes = System.Windows.Media.Brushes;

namespace MaoTouGu.JuXiaoYou.Prototypings
{

    [Associate(View = typeof(PrototypingView), ViewModel = typeof(PrototypingViewModel))]
    public partial class PrototypingView : ForestPage
    {
        public PrototypingView()
        {
            InitializeComponent();
        }
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement fe)
            {
                return;
            }

            if (e.ClickCount < 2)
            {
                return;
            }

            var btn      = new Button { Content = "关闭", };
            var border = new System.Windows.Controls.Border
            {
                Background = new VisualBrush { Visual = fe, Stretch = Stretch.None},
                Width      = fe.RenderSize.Width,
                Height     = fe.RenderSize.Height,
            };

            border.Measure(fe.RenderSize);
            border.Arrange(new Rect(fe.RenderSize));
            border.UpdateLayout();

            btn.Click += (_, _) =>
                         {
                             WindowBehavior.CloseFlyout(Xaml.FindVisualParent<Window>(this));
                         };

            WindowBehavior.FlyoutObject(Xaml.FindVisualParent<Window>(this), border);
        }
    }
}
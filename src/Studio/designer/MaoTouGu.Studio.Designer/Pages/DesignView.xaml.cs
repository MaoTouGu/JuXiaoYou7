using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using MaoTouGu.JuXiaoYou.Controls;
using MaoTouGu.Studio.Database.Topology;

namespace MaoTouGu.JuXiaoYou.Pages
{

    [Associate(View = typeof(DesignView), ViewModel = typeof(DesignViewModel))]
    public partial class DesignView : ForestPage
    {
        private ResizeAdorner _adorner;

        public DesignView()
        {
            InitializeComponent();
        }

        private void Button_ReleaseCapture(object sender, RoutedEventArgs e)
        {
            //
            // 如果锁定了当前选择的元素。
            if (sender is not FrameworkElement { DataContext: DesignViewModel{ Block : TypographyBlockVPO { IsLock: true } a }})
            {
                return;
            }

            if (_adorner?.AdornedElement is not FrameworkElement { DataContext: TypographyBlock b } ||
                !ReferenceEquals(b, a))
            {
                return;
            }

            var layer = AdornerLayer.GetAdornerLayer(Items);

            if (layer is null)
            {
                return;
            }

            layer.Remove(_adorner);
            _adorner = null;
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement fe || e.ClickCount < 2)
            {
                return;
            }

            if (fe.DataContext is not TypographyBlockVPO { IsLock: false } block)
            {
                return;
            }

            //
            //
            ViewModel<DesignViewModel>().Block = block;

            //
            //
            var resizeAdorner = new ResizeAdorner(fe);
            var layer         = AdornerLayer.GetAdornerLayer(Items);

            if (layer is null)
            {
                return;
            }

            if (_adorner is not null)
            {
                layer.Remove(_adorner);
            }

            layer.Add(resizeAdorner);
            _adorner = resizeAdorner;

        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not FrameworkElement fe)
            {
                return;
            }

            var Canvas = Xaml.FindVisualParent<Canvas>(fe);
            var pos    = Canvas.TranslatePoint(e.GetPosition(fe), fe);

            if (fe.DataContext is TypographyBlock b)
            {
                b.X = (int)(pos.X / 20);
                b.Y = (int)(pos.Y / 20);
            }
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe)
            {
                fe.MouseMove -= Control_MouseMove;
            }
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (sender is FrameworkElement fe)
            {
                fe.MouseMove -= Control_MouseMove;
            }
        }

        private TabPanel _tabPanel;

        private void TabControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not FrameworkElement fe)
            {
                return;
            }

            if (_tabPanel is null)
            {
                _tabPanel = Xaml.FindVisualParent<TabPanel>(fe);

                fe.SizeChanged -= TabControl_OnSizeChanged;
            }

            if (_tabPanel is null)
            {
                return;
            }
            //
            // 让上级窗口可以直到Margin
            var margin = new Thickness(_tabPanel.ActualWidth, 0, 0, 0);
            var window = Xaml.FindVisualParent<StandaloneWindow>(this);
            var grid   = Xaml.FindVisualChild<Grid>(window, x => x.Name == "TitleBar", 4);

            grid.Margin    =  margin;
            fe.SizeChanged -= TabControl_OnSizeChanged;

        }
    }
}
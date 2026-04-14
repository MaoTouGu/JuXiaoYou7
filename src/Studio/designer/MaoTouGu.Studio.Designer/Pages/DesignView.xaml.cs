using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using MaoTouGu.JuXiaoYou.Controls;
using MaoTouGu.Studio.Database.Topology;

namespace MaoTouGu.JuXiaoYou.Pages
{

    [Associate(View = typeof(DesignView), ViewModel = typeof(DesignViewModel))]
    public partial class DesignView : ForestPage
    {
        private ResizeAdorner _adorner;
        private TabPanel      _tabPanel;

        public DesignView()
        {
            InitializeComponent();

        }

        private void Button_ReleaseCapture(object sender, RoutedEventArgs e)
        {
            //
            // 如果锁定了当前选择的元素。
            if (sender is not FrameworkElement { DataContext: DesignViewModel { Block : TypographyBlockVPO { IsLock: true } a } })
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
            AdornerLayer layer;
            var          dc = ViewModel<DesignViewModel>();

            if (sender is not FrameworkElement fe || e.ClickCount < 2 || fe.DataContext is not TypographyBlockVPO { IsLock: false } block)
            {
                layer = AdornerLayer.GetAdornerLayer(Items);

                if (layer is null || _adorner is null)
                {
                    return;
                }

                layer.Remove(_adorner);
                dc.Block = null;
                return;
            }

            //
            //
            dc.Block = block;

            //
            //
            var resizeAdorner = new ResizeAdorner(fe);

            layer = AdornerLayer.GetAdornerLayer(Items);

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
        private void ScrollViewer_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var viewer = (ScrollViewer)sender;

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                viewer.ScrollToVerticalOffset(e.Delta + viewer.VerticalOffset);
                e.Handled = true;
            }
            else if(Keyboard.Modifiers == ModifierKeys.Shift)
            {
                viewer.ScrollToHorizontalOffset(e.Delta + viewer.HorizontalOffset);
                e.Handled = true;
            }
            
        }


        private void CommandBinding_IncreaseZoom(object sender, ExecutedRoutedEventArgs e)
        {
            var dc        = ViewModel<DesignViewModel>();
            var baseScale = (int)(dc.IntScale / 10d * 10);

            dc.IntScale = Math.Clamp(baseScale + 10, 0, 400);
        }

        private void CommandBinding_DecreaseZoom(object sender, ExecutedRoutedEventArgs e)
        {
            var dc        = ViewModel<DesignViewModel>();
            var baseScale = (int)(dc.IntScale / 10d * 10);

            dc.IntScale = Math.Clamp(baseScale - 10, 0, 400);
        }
    }
}
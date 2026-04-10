using System.Windows.Documents;

namespace MaoTouGu.Shells.Controls
{
    public class FlyoutAdornerCanvas : Adorner
    {
        private static readonly Brush Mask      = new SolidColorBrush(Color.FromArgb(0xA8, 00, 00, 00));
        private static readonly Pen   Highlight = new Pen(Brushes.Crimson, 3);

        private readonly FrameworkElement  _flyoutContext;
        private readonly Placement        _placement;
        private readonly FrameworkElement _target;
        private readonly Border           _dummyBorder;
        private readonly VisualCollection _collection;
        private readonly FlyoutObject     _dataContext;

        private Rect _targetRect;

        //
        // @alwaysContentPresenter表示需要装饰的内容，一般来说都是与Window等尺寸的元素。
        // @target表示需要装饰的元素，一般是引导所标记的控件。

        public FlyoutAdornerCanvas(UIElement alwaysContentPresenter, FlyoutObject dataContext, FrameworkElement target, FrameworkElement wrapper) : base(alwaysContentPresenter)
        {
            _target      = target;
            _dataContext = dataContext;
            _placement   = dataContext.Placement;

            _collection    = new VisualCollection(this);
            _dummyBorder   = new Border { Background = new VisualBrush { Visual = _target, Stretch = Stretch.None } };
            _flyoutContext = wrapper;

            _collection.Add(_flyoutContext);
            _collection.Add(_dummyBorder);

            //
            //
            _flyoutContext.DataContext = dataContext;

            //
            //
            AddLogicalChild(_flyoutContext);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            //
            // 获得相对于AdornedElement的位置。
            _targetRect = Xaml.GetPosition(AdornedElement, _target);

            _flyoutContext.Measure(constraint);
            _dummyBorder.Measure(_targetRect.Size);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //
            // 计算相对大小。

            double x;
            double y;


            switch (_placement)
            {
                case Placement.Left:
                    x = _targetRect.X - _flyoutContext.ActualWidth - 3;
                    y = _targetRect.Y;
                    break;
                case Placement.Right:
                    x = _targetRect.X + _targetRect.Width + 3;
                    y = _targetRect.Y;
                    break;
                case Placement.Top:
                    x = _targetRect.X;
                    y = _targetRect.Y - _flyoutContext.ActualHeight - 3;
                    break;
                case Placement.Bottom:
                default:
                    x = _targetRect.X;
                    y = _targetRect.Y + _targetRect.Height + 3;
                    break;
            }

            //
            // 横坐标不应该超出边界
            x = Math.Clamp(x, 0, AdornedElement.RenderSize.Width  - _flyoutContext.ActualWidth);
            y = Math.Clamp(y, 0, AdornedElement.RenderSize.Height - _flyoutContext.ActualHeight);

            //
            // 布局。
            _flyoutContext.Arrange(
                                   new Rect(
                                            x,
                                            y,
                                            _flyoutContext.ActualWidth,
                                            _flyoutContext.ActualHeight));
            _dummyBorder.Arrange(_targetRect);


            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            //
            // 绘制半透明遮罩层。
            drawingContext.DrawRectangle(Mask, null, new Rect(AdornedElement.RenderSize));

            //
            // 绘制控件大小的边框
            drawingContext.DrawRectangle(null, Highlight, _targetRect);
        }

        protected override Visual GetVisualChild(int index) => _collection[index];

        protected override int VisualChildrenCount => _collection.Count;
    }
}
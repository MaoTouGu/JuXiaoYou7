using System.Windows.Documents;

namespace MaoTouGu.Shells.Controls.Adorners
{
    public class SurroundingCanvasAdorner : Adorner
    {
        private static readonly Brush Mask      = new SolidColorBrush(Color.FromArgb(0xA8, 00, 00, 00));
        private static readonly Pen   Highlight = new Pen(Brushes.Crimson, 3);

        private readonly FrameworkElement _target;
        private readonly Border           _dummyBorder;
        private readonly VisualCollection _collection;
        private readonly Surrounding      _dataContext;

        private Rect _targetRect;

        //
        // @alwaysContentPresenter表示需要装饰的内容，一般来说都是与Window等尺寸的元素。
        // @target表示需要装饰的元素，一般是引导所标记的控件。

        public SurroundingCanvasAdorner(UIElement alwaysContentPresenter, Surrounding dataContext, FrameworkElement target) : base(alwaysContentPresenter)
        {
            _target      = target;
            _dataContext = dataContext;

            _collection  = new VisualCollection(this);
            _dummyBorder = new Border { Background = new VisualBrush { Visual = _target, Stretch = Stretch.None } };

            _collection.Add(dataContext.Left);
            _collection.Add(dataContext.Top);
            _collection.Add(dataContext.Right);
            _collection.Add(dataContext.Bottom);
            _collection.Add(_dummyBorder);

            //
            //
            AddLogicalChild(dataContext.Left);
            AddLogicalChild(dataContext.Top);
            AddLogicalChild(dataContext.Right);
            AddLogicalChild(dataContext.Bottom);
        }


        void ArrangeLeft()
        {
            _dataContext.Left
                        .Arrange(
                                 new Rect(
                                          _dataContext.XGap,
                                          _targetRect.Y,
                                          _dataContext.Left.ActualWidth,
                                          _dataContext.Left.ActualHeight));
        }

        void ArrangeRight()
        {
            _dataContext.Right
                        .Arrange(
                                 new Rect(
                                          _targetRect.Right + _dataContext.XGap,
                                          _targetRect.Y,
                                          _dataContext.Right.ActualWidth,
                                          _dataContext.Right.ActualHeight));
        }

        void ArrangeTop()
        {
            _dataContext.Top
                        .Arrange(
                                 new Rect(
                                          _targetRect.X,
                                          _dataContext.YGap,
                                          _dataContext.Top.ActualWidth,
                                          _dataContext.Top.ActualHeight));
        }

        void ArrangeBottom()
        {
            _dataContext.Bottom
                        .Arrange(
                                 new Rect(
                                          _targetRect.X,
                                          _targetRect.Bottom + _dataContext.YGap,
                                          _dataContext.Bottom.ActualWidth,
                                          _dataContext.Bottom.ActualHeight));
        }


        protected override Size MeasureOverride(Size constraint)
        {
            //
            // 获得相对于AdornedElement的位置。
            _targetRect = Xaml.GetPosition(AdornedElement, _target);

            var remainW = constraint.Width  - _targetRect.Width;
            var remainH = constraint.Height - _targetRect.Height;

            var w = (remainW - (4 * _dataContext.XGap)) / 2d;
            var h = (remainH - (4 * _dataContext.YGap)) / 2d;

            var x = new Size(w, _targetRect.Height);
            var y = new Size(_targetRect.Width, h);

            _dataContext.Left.Height = _dataContext.Right.Height = x.Height;
            _dataContext.Left.Width  = _dataContext.Right.Width  = x.Width;

            _dataContext.Left.Measure(x);
            _dataContext.Right.Measure(x);

            _dataContext.Top.Height = _dataContext.Bottom.Height = y.Height;
            _dataContext.Top.Width  = _dataContext.Bottom.Width  = y.Width;
            _dataContext.Top.Measure(y);
            _dataContext.Bottom.Measure(y);

            _dummyBorder.Measure(_targetRect.Size);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //
            // 计算相对大小。

            ArrangeLeft();
            ArrangeRight();
            ArrangeTop();
            ArrangeBottom();
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
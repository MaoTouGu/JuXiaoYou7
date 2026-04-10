using System.Windows.Documents;
using MaoTouGu.Shells.Behaviors;

namespace MaoTouGu.Shells.Adorners
{
    public sealed class ControlWrapperAdorner : Adorner
    {
        private static readonly Brush Mask = new SolidColorBrush(Color.FromArgb(0xA8, 00, 00, 00));

        private readonly VisualCollection _collection;

        public ControlWrapperAdorner(UIElement alwaysContentPresenter, FrameworkElement target) : base(alwaysContentPresenter)
        {
            Focusable     = true;
            TargetElement = target;

            target.Focus();
            _collection = new VisualCollection(this);
            _collection.Add(target);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (e.Handled)
            {
                return;
            }

            var window = Xaml.FindVisualParent<Window>(AdornedElement);

            
            WindowBehavior.CloseFlyout(window);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            TargetElement.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = (finalSize.Width  - TargetElement.DesiredSize.Width)  / 2d;
            var y = (finalSize.Height - TargetElement.DesiredSize.Height) / 2d;

            TargetElement.Arrange(new Rect(new Point(x, y), TargetElement.DesiredSize));
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            //
            // 绘制半透明遮罩层。
            drawingContext.DrawRectangle(Mask, null, new Rect(AdornedElement.RenderSize));
        }

        protected override Visual GetVisualChild(int index) => _collection[index];
        protected override int VisualChildrenCount => _collection.Count;

        public FrameworkElement TargetElement { get; }
    }
}
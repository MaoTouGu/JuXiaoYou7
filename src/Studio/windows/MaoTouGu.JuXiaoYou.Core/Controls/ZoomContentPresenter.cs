
using System.Windows.Markup;

namespace MaoTouGu.Studio.Controls
{

    /// <summary>
    /// Zoom content presenter control.
    /// </summary>
    public sealed class ZoomContentPresenter : ContentPresenter, IAddChild
    {
        public event ContentSizeChangedHandler ContentSizeChanged;

        private Size _contentSize;
        
        void IAddChild.AddChild(object value)
        {
            if (value is FrameworkElement fe)
            {
                Content = fe;
            }
        }
        void IAddChild.AddText(string text)
        {
            
        }

        public Size ContentSize
        {
            get => _contentSize;
            private set
            {
                if (_contentSize == value)
                    return;

                _contentSize = value;
                ContentSizeChanged?.Invoke(this, _contentSize);
            }
        }

        private const int InfiniteSize = 1000000000;

        /// <inheritdoc />
        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(new Size(double.PositiveInfinity, double.PositiveInfinity));

            return new Size(
                            double.IsInfinity(constraint.Width) ? InfiniteSize : constraint.Width,
                            double.IsInfinity(constraint.Height) ? InfiniteSize : constraint.Height);
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var uiElement = VisualChildrenCount > 0
                ? VisualTreeHelper.GetChild(this, 0) as UIElement
                : null;
            if (uiElement is null)
                return arrangeSize;

            ContentSize = uiElement.DesiredSize;
            uiElement.Arrange(new Rect(uiElement.DesiredSize));

            return arrangeSize;
        }
    }
}
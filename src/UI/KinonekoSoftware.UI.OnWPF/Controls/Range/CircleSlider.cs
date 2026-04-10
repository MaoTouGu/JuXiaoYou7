using System.Windows.Input;
using System.Windows.Media;
using MaoTouGu.Foundation.Mathematics;

namespace KinonekoSoftware.UI.Controls.Range
{
    public sealed class CircleSlider : Control
    {

        public static readonly DependencyProperty HighlightProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty OuterMarginProperty;

        static CircleSlider()
        {
            HighlightProperty =
                DependencyProperty.Register(
                                            nameof(Highlight),
                                            typeof(Brush),
                                            typeof(CircleSlider),
                                            new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
            ValueProperty =
                DependencyProperty.Register(
                                            nameof(Value),
                                            typeof(double),
                                            typeof(CircleSlider),
                                            new UIPropertyMetadata(0d, OnValueChanged, CoerceValueChanged));           
            OuterMarginProperty =
                DependencyProperty.Register(
                                            nameof(OuterMargin),
                                            typeof(double),
                                            typeof(CircleSlider),
                                            new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsRender));

        }

        private static object CoerceValueChanged(DependencyObject d, object basevalue)
        {
            return (basevalue is double v ? v : 0d) % 360;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FrameworkElement)?.InvalidateVisual();
        }

        private void CalculateValueFromPosition(ref Point pos)
        {
            var centerX = RenderSize.Width  / 2d;
            var centerY = RenderSize.Height / 2d;
            Value = (int)CoordinateSystem.GetAngle(pos.X - centerX, pos.Y - centerY);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var background  = Background;
            var borderBrush = BorderBrush;
            var highlight   = IsMouseOver ? Highlight : borderBrush;
            var renderSize  = RenderSize;

            //
            //
            var v = (360 - Value) / 180d * Math.PI;

            //
            //
            var r           = Math.Min(renderSize.Width, renderSize.Height) / 2d;
            var outerMargin = r - OuterMargin;
            var center      = new Point(r, r);

            CoordinateSystem.CirculationPositionConstraint(outerMargin, v, r, r, out var x, out var y);

            //
            // 绘制基础背景
            drawingContext.DrawEllipse(background, null, center, r, r);
            
            //
            // Outer Margin
            drawingContext.DrawEllipse(null, new Pen(borderBrush, 2), center, outerMargin, outerMargin);
            
            //
            // Thumb
            drawingContext.DrawEllipse(highlight, null, new Point(x, y), 8, 8);

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            CalculateValueFromPosition(ref pos);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(this);
                CalculateValueFromPosition(ref pos);
            }
            else
            {
                InvalidateVisual();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            InvalidateVisual();
            base.OnMouseLeave(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            InvalidateVisual();
            base.OnLostFocus(e);
        }


        public double OuterMargin
        {
            get => (double)GetValue(OuterMarginProperty);
            set => SetValue(OuterMarginProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public Brush Highlight
        {
            get => (Brush)GetValue(HighlightProperty);
            set => SetValue(HighlightProperty, value);
        }
    }
}
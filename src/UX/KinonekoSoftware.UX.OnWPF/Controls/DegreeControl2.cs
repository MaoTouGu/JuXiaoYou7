using System.Windows.Input;

namespace KinonekoSoftware.UX
{
    public class DegreeControl2 : Control
    {

        public static readonly DependencyProperty ThumbGapProperty
            = DependencyProperty.Register(
                                          nameof(ThumbGap),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(2, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ThumbWidthProperty
            = DependencyProperty.Register(
                                          nameof(ThumbWidth),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(16, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty ThumbHeightProperty
            = DependencyProperty.Register(
                                          nameof(ThumbHeight),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(9, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register(
                                          nameof(Value),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(2, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MaximumProperty
            = DependencyProperty.Register(
                                          nameof(Maximum),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MinimumProperty
            = DependencyProperty.Register(
                                          nameof(Minimum),
                                          typeof(int),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty RadiusProperty
            = DependencyProperty.Register(
                                          nameof(Radius),
                                          typeof(double),
                                          typeof(DegreeControl2),
                                          new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));

        private readonly Point[] _Position = new Point[32];

        protected override Size MeasureOverride(Size constraint)
        {
            var max    = Math.Clamp(Maximum, 1, 32);
            var gap    = Math.Clamp(ThumbGap, 2, 20);
            var width  = Math.Clamp(ThumbWidth, 10, 96);
            var height = Math.Clamp(ThumbHeight, 9, 30);
            var column = (int)(constraint.Width / (gap + width));
            var row    = (max + column - 1) / column;

            var w = double.IsInfinity(constraint.Width) ? column * width + (column - 1) * gap : constraint.Width;
            return new Size(w, row * height + (row - 1) * gap);
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (Visibility != Visibility.Visible)
            {
                return;
            }


            var max = Math.Clamp(Maximum, 1, 32);
            var min = Math.Clamp(Minimum, 0, 32);
            var x   = 0d;


            if (min > max)
            {
                (max, min) = (min, max);
            }

            var v      = Math.Clamp(Value, min, max);
            var gap    = Math.Clamp(ThumbGap, 2, 20);
            var size   = Math.Clamp(ThumbWidth, 10, 96);
            var height = Math.Clamp(ThumbHeight, 9, 96);
            var bg     = Background                  ?? new SolidColorBrush(Colors.Gray);
            var fg     = (BorderBrush ?? Foreground) ?? new SolidColorBrush(Colors.Olive);
            var r      = Radius;
            var column = (int)(ActualWidth / (gap + size));
            var y      = 0d;

            for (var i = 0; i < max;)
            {
                for (var c = 0; c < column && i < max; c++)
                {
                    _Position[i] = new Point(x, y);
                    var b = v > i && v > 0 ? fg : bg;
                    dc.DrawRoundedRectangle(b, null, new Rect(x, y, size, height), r, r);

                    x += gap + size;
                    i++;
                }

                x =  0d;
                y += size + gap;
            }

            base.OnRender(dc);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var size = Math.Clamp(ThumbWidth, 10, 96) + ThumbGap;
            var p    = e.GetPosition(this);
            var max  = Maximum;
            for (var i = 0; i < max; i++)
            {
                var p1 = _Position[i];

                if (p1.X          <= p.X &&
                    (p1.X + size) >= p.X &&
                    p1.Y          <= p.Y &&
                    (p1.Y + size) >= p.Y)
                {

                    if (i == 0)
                    {
                        if (Value == 0)
                        {
                            Value = 1;
                        }
                        else
                        {
                            Value = Value > 1 ? 1 : 0;
                        }
                    }
                    else
                    {
                        Value = i + 1;
                    }
                    break;
                }
            }

            base.OnMouseDown(e);
        }

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public int ThumbHeight
        {
            get => (int)GetValue(ThumbHeightProperty);
            set => SetValue(ThumbHeightProperty, value);
        }
        public int ThumbWidth
        {
            get => (int)GetValue(ThumbWidthProperty);
            set => SetValue(ThumbWidthProperty, value);
        }
        public int ThumbGap
        {
            get => (int)GetValue(ThumbGapProperty);
            set => SetValue(ThumbGapProperty, value);
        }
    }
}
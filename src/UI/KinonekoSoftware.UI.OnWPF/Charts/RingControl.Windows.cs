namespace KinonekoSoftware.UI.Charts
{
#if WINDOWS

    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class RingControl
    {

        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty StrokeProperty;
        public static readonly DependencyProperty StrokeThicknessProperty;
        public static readonly DependencyProperty CapStyleProperty;

        static RingControl()
        {
            ValueProperty = DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(RingControl),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty = DependencyProperty.Register(
                nameof(Stroke),
                typeof(Brush),
                typeof(RingControl),
                new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeThicknessProperty = DependencyProperty.Register(
                nameof(StrokeThickness),
                typeof(int),
                typeof(RingControl),
                new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsRender));
            MinimumProperty = DependencyProperty.Register(
                nameof(Minimum),
                typeof(double),
                typeof(RingControl),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));
            MaximumProperty = DependencyProperty.Register(
                nameof(Maximum),
                typeof(double),
                typeof(RingControl),
                new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.AffectsRender));
            CapStyleProperty = DependencyProperty.Register(
                nameof(CapStyle),
                typeof(PenLineCap),
                typeof(RingControl),
                new FrameworkPropertyMetadata(default(PenLineCap), FrameworkPropertyMetadataOptions.AffectsRender));
        }

        private partial PathGeometry CreateGeometry(double radius, bool isLarge, bool isClose, Point start, Point end)
        {
            var size  = new Size(radius, radius);
            var segment = new ArcSegment
            {
                IsLargeArc     = isLarge,
                Size           = size,
                Point          = end,
                SweepDirection = SweepDirection.Clockwise,
            };

            var geometry = new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
                        StartPoint = start,
                        IsClosed   = isClose,
                        Segments = new PathSegmentCollection
                        {
                            segment,
                        },
                    },
                },
            };
            return geometry;
        }
        
        private Pen CreateCapStylePen(Brush brush, double thickness)
        {
            return new Pen(brush, thickness)
            {
                EndLineCap   = CapStyle,
                StartLineCap = CapStyle,
            };
        }
        
        protected void OnValueChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            //
            // 准备数据
            Prepare();
            
            //
            // 渲染数据
            Draw(drawingContext);
            
            base.OnRender(drawingContext);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        
        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }
        
        public int StrokeThickness
        {
            get => (int)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public PenLineCap CapStyle
        {
            get => (PenLineCap)GetValue(CapStyleProperty);
            set => SetValue(CapStyleProperty, value);
        }
    }

#endif
}
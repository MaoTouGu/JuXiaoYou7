
// ReSharper disable RedundantArgumentDefaultValue

namespace KinonekoSoftware.UI.Charts
{
    using Avalonia.Media;

    public partial class StackedRingControl
    {
        public static readonly StyledProperty<ChartSeries>  ValuesProperty;
        public static readonly StyledProperty<ChartPalette> PaletteProperty;
        public static readonly StyledProperty<int>          StrokeThicknessProperty;
        public static readonly StyledProperty<PenLineCap>      CapStyleProperty;

        static StackedRingControl()
        {
            ValuesProperty          = AvaloniaProperty.Register<StackedRingControl, ChartSeries>(nameof(Values));
            PaletteProperty         = AvaloniaProperty.Register<StackedRingControl, ChartPalette>(nameof(Palette));
            StrokeThicknessProperty = AvaloniaProperty.Register<StackedRingControl, int>(nameof(StrokeThickness), 8);
            CapStyleProperty        = AvaloniaProperty.Register<StackedRingControl, PenLineCap>(nameof(CapStyle));
            PaletteProperty.Changed.Subscribe(OnRenderChanged);
            StrokeThicknessProperty.Changed.Subscribe(OnRenderChanged);
            ValuesProperty.Changed.Subscribe(OnRenderChanged);
            CapStyleProperty.Changed.Subscribe(OnRenderChanged);
        }

        private static void OnRenderChanged(AvaloniaPropertyChangedEventArgs x)
        {
            var rc = (RingControl)x.Sender;
            rc.InvalidateVisual();
        }
        private Pen CreateCapStylePen(Brush brush, double thickness)
        {
            return new Pen(brush, thickness)
            {
                LineCap   = CapStyle,
            };
        }

        private partial PathGeometry CreateGeometry(double radius, bool isLarge, bool isClose, Point start, Point end)
        {

            var size = new Size(radius, radius);
            var segment = new ArcSegment
            {
                IsLargeArc     = isLarge,
                Size           = size,
                Point          = end,
                SweepDirection = SweepDirection.Clockwise,
            };

            var geometry = new PathGeometry
            {
                Figures = new PathFigures
                {
                    new PathFigure
                    {
                        StartPoint = start,
                        IsClosed   = isClose,
                        Segments = new PathSegments
                        {
                            segment,
                        },
                    },
                },
            };
            return geometry;
        }
        
        
        public override void Render(DrawingContext context)
        {

            //
            // 准备数据
            Prepare();

            //
            // 渲染数据
            Draw(context);

            base.Render(context);
        }

        
        public ChartSeries Values
        {
            get => GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }
        
        
        public PenLineCap CapStyle
        {
            get => GetValue(CapStyleProperty);
            set => SetValue(CapStyleProperty, value);
        }

        public int StrokeThickness
        {
            get => GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public ChartPalette Palette
        {
            get => GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }
    }

}
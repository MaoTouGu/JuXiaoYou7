
// ReSharper disable RedundantArgumentDefaultValue

namespace KinonekoSoftware.UI.Charts
{
    using Avalonia.Utilities;
    using Avalonia.Media;

    public partial class RingControl
    {
        public static readonly StyledProperty<double>     ValueProperty;
        public static readonly StyledProperty<double>     MinimumProperty;
        public static readonly StyledProperty<double>     MaximumProperty;
        public static readonly StyledProperty<Brush>      StrokeProperty;
        public static readonly StyledProperty<int>        StrokeThicknessProperty;
        public static readonly StyledProperty<PenLineCap> CapStyleProperty;

        static RingControl()
        {
            ValueProperty              = AvaloniaProperty.Register<RingControl, double>(nameof(Value));
            MinimumProperty            = AvaloniaProperty.Register<RingControl, double>(nameof(Minimum), 0d);
            MaximumProperty            = AvaloniaProperty.Register<RingControl, double>(nameof(Maximum), 100d);
            StrokeProperty             = AvaloniaProperty.Register<RingControl, Brush>(nameof(Stroke));
            StrokeThicknessProperty    = AvaloniaProperty.Register<RingControl, int>(nameof(StrokeThickness), 8);
            CapStyleProperty = AvaloniaProperty.Register<RingControl, PenLineCap>(nameof(CapStyle));
            StrokeProperty.Changed
                          .Subscribe(OnRenderChanged);
            StrokeThicknessProperty.Changed
                                   .Subscribe(OnRenderChanged);
            MinimumProperty.Changed
                           .Subscribe(OnRenderChanged);
            ValueProperty.Changed
                         .Subscribe(OnRenderChanged);
            CapStyleProperty.Changed
                                      .Subscribe(OnRenderChanged);
            MaximumProperty.Changed
                           .Subscribe(OnRenderChanged);
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


        public PenLineCap CapStyle
        {
            get => GetValue(CapStyleProperty);
            set => SetValue(CapStyleProperty, value);
        }


        public double Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }


        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double Minimum
        {
            get => GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public int StrokeThickness
        {
            get => GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public Brush Stroke
        {
            get => GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }
    }

}
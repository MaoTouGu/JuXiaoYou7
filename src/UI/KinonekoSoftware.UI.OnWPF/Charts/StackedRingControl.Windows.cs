
// ReSharper disable RedundantArgumentDefaultValue

namespace KinonekoSoftware.UI.Charts
{
#if WINDOWS
    using System.Windows.Media;

    public partial class StackedRingControl
    {
        public static readonly DependencyProperty PaletteProperty;
        public static readonly DependencyProperty StrokeThicknessProperty;
        public static readonly DependencyProperty CapStyleProperty;
        public static readonly DependencyProperty ValuesProperty;
        
        static StackedRingControl()
        {
            PaletteProperty = DependencyProperty.Register(
                nameof(Palette),
                typeof(ChartPalette),
                typeof(StackedRingControl),
                new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeThicknessProperty = DependencyProperty.Register(
                nameof(StrokeThickness),
                typeof(int),
                typeof(StackedRingControl),
                new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsRender));
            CapStyleProperty = DependencyProperty.Register(
                nameof(CapStyle),
                typeof(PenLineCap),
                typeof(StackedRingControl),
                new FrameworkPropertyMetadata(default(PenLineCap), FrameworkPropertyMetadataOptions.AffectsRender));
            ValuesProperty = DependencyProperty.Register(
                nameof(Values),
                typeof(ChartSeries),
                typeof(StackedRingControl),
                new PropertyMetadata(default(ChartSeries), OnValueChanged));
        }
        
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rc = (StackedRingControl)d;
            
            if (e.NewValue is IDataSourceTracker nDST)
            {
                nDST.DataChanged += rc.OnValueChanged;
            }
            
            if (e.OldValue is IDataSourceTracker oDST)
            {
                oDST.DataChanged -= rc.OnValueChanged;
            }
            
            rc.OnValueChanged(d, EventArgs.Empty);
        }
        
        protected void OnValueChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
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


        protected override void OnRender(DrawingContext drawingContext)
        {
            //
            // 准备数据
            Prepare();

            var guide = new GuidelineSet();

            if (_data is not null)
            {
                for (var i = 0; i < _data.Parts.Length; i++)
                {
                    var part = _data.Parts[i];
                    if (i == 0)
                    {
                        guide.GuidelinesX.Add(part.Start.X);
                        guide.GuidelinesY.Add(part.Start.Y);
                    }
                    guide.GuidelinesX.Add(part.End.X);
                    guide.GuidelinesY.Add(part.End.Y);
                }

            }
            
            drawingContext.PushGuidelineSet(guide);
            //
            // 渲染数据
            Draw(drawingContext);
            drawingContext.Pop();


            base.OnRender(drawingContext);
        }

        public ChartSeries Values
        {
            get => (ChartSeries)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }


        public ChartPalette Palette
        {
            get => (ChartPalette)GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
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
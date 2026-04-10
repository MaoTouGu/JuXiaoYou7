using MaoTouGu.Foundation.Mathematics;
namespace MaoTouGu.Shells.Controls
{
    public sealed class FlyoutBorder : Border
    {
        private static readonly object Uniform;

        public static readonly DependencyProperty PlacementProperty;

        static FlyoutBorder()
        {
            Uniform = new Thickness(16);
            PlacementProperty = DependencyProperty.Register(
                                                            nameof(Placement),
                                                            typeof(Placement),
                                                            typeof(FlyoutBorder),
                                                            new FrameworkPropertyMetadata(default(Placement),
                                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                                          FrameworkPropertyMetadataOptions.AffectsRender));
        }

        private StreamGeometry _geometry;
        public FlyoutBorder()
        {
            //
            // 避免频繁装箱
            SetValue(BorderThicknessProperty, Uniform);
            SetValue(PaddingProperty, Uniform);
        }



        protected override Size ArrangeOverride(Size finalSize)
        {
            var s = base.ArrangeOverride(finalSize);


            const double arrow = 16d;

            var p         = Placement;
            var thickness = BorderThickness;
            var w         = s.Width  - thickness.Left - thickness.Right;
            var h         = s.Height - thickness.Top  - thickness.Bottom;

            
            _geometry = new StreamGeometry();
            using (var ctx = _geometry.Open())
            {
                // 箭头
                Point p1, p2, p3;

                var min = 2 * arrow;
                var mid = 3 * arrow;
                var max = 4 * arrow;

                switch (p)
                {
                    case Placement.Top:
                        p1 = new Point(min, thickness.Top         + 1);
                        p2 = new Point(max, thickness.Top         + 1);
                        p3 = new Point(mid, thickness.Top - arrow + 1);
                        break;
                    case Placement.Bottom:
                        p1 = new Point(min, h + thickness.Bottom         - 1);
                        p2 = new Point(max, h + thickness.Bottom         - 1);
                        p3 = new Point(mid, h + arrow + thickness.Bottom - 1);
                        break;
                    case Placement.Left:
                        p1 = new Point(thickness.Left         + 1, min);
                        p2 = new Point(thickness.Left         + 1, max);
                        p3 = new Point(thickness.Left - arrow + 1, mid);
                        break;
                    case Placement.Right:
                    default:
                        var r = mid - thickness.Right - 1;
                            
                        p1 = new Point(w + r - arrow, min);
                        p2 = new Point(w + r - arrow, max);
                        p3 = new Point(w + r, mid);
                        break;
                }
                ctx.BeginFigure(p1, true, true);
                ctx.LineTo(p2, true, false);
                ctx.LineTo(p3, true, false);
                ctx.Close();
            }
            _geometry.Freeze();
            return s;
        }



        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawGeometry(Background, null, _geometry);
        }

        public Placement Placement
        {
            get => (Placement)GetValue(PlacementProperty);
            set => SetValue(PlacementProperty, value);
        }
    }
}
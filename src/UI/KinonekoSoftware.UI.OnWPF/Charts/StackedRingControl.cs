using KinonekoSoftware.UI.Geometries;

namespace KinonekoSoftware.UI.Charts
{
    public partial class StackedRingControl : ChartControl
    {
        private const double RevisionFactor = 0.008d;
        
        public class RenderData
        {
            public double Diameter { get; internal set; }
            public double Radius   { get; internal set; }
            public double Width    { get; internal set; }
            public double Height   { get; internal set; }
            public Part[] Parts    { get; internal set; }
            public Point  Center   { get; internal set; }
        }

        public class Part
        {
            public string Color   { get; internal set; }
            public Point  Start   { get; internal set; }
            public Point  End     { get; internal set; }
            public double Percent { get; internal set; }
            public double Value   { get; internal set; }
            public bool   IsClose { get; internal set; }
            public bool   IsLarge { get; internal set; }
        }

        // private static readonly Brush      TransparentBrush;
        private RenderData _data;

        private partial PathGeometry CreateGeometry(double radius, bool isLarge, bool isClose, Point start, Point end);

        protected void Prepare()
        {
            var values = Values ?? new ChartSeries();

            if (values.Count == 0)
            {
                return;
            }

            var half = Math.Clamp(StrokeThickness, 2, 512);
            GetSize(out var w, out var h);
            var d        = Math.Min(w, h);
            var r        = d / 2d - half;
            var center   = new Point(r + half, r + half);
            var max      = values.Sum(x => x.Value);
            var remain   = values.Count;
            var revision = 0d;
            var a        = 0d;

            _data = new RenderData
            {
                Diameter = d,
                Center   = center,
                Width    = w,
                Height   = h,
                Radius   = r,
                Parts    = new Part[values.Count],
            };

            
            
            
            //
            // 在过去渲染的时候，小于1%的部分都会以真实的大小显示
            // 这会导致该部分的实际视觉效果非常的微弱，所以我们决定使用clamp来修正
            // 所有小于1%的Series都会算成1%，并把1% - percent的实际值累加起来，存储于revision当中，
            // revision这部分的值将会被所有大于1%的部分分摊。
            foreach (var series in values)
            {
                if (series is null)
                {
                    remain -= 1;
                    continue;
                }

                var v       = Math.Clamp(series.Value, 0, max);
                var percent = v / max;

                if (DoubleStatic.LessThan(percent, RevisionFactor))
                {
                    remain   -= 1;
                    revision += (RevisionFactor - percent);
                }
            }
            
            
            //
            // 均摊
            revision /= remain;

            for (var i = 0; i < values.Count; i++)
            {
                var series = values[i];

                if (series is null)
                {
                    continue;
                }

                var v        = Math.Clamp(series.Value, 0, max);
                var percent  = v / max;
                
                

                if (DoubleStatic.GreaterThan(percent, RevisionFactor))
                {
                    //
                    // 修正
                    percent -= revision;
                }
                else
                {
                    percent = RevisionFactor;
                }

                //
                // 再次累加
                var endAngle = percent * 360d;
                a += endAngle;


                if (i == 0)
                {
                    ArcAssist.GetArcSegments(endAngle, r, out _, out _, out var endPointX, out var endPointY);
                    CircularAssits.GetPointFromAngle(0, r, out var startPointX, out var startPointY);

                    _data.Parts[i] = new Part
                    {
                        Start   = new Point(startPointX + half, startPointY + half),
                        End     = new Point(endPointX   + half, endPointY   + half),
                        IsLarge = endAngle >= 180,
                        IsClose = a        >= 359d,
                        Value   = v,
                        Color   = series.Color,
                        Percent = percent,
                    };
                }
                else
                {
                    ArcAssist.GetArcSegments(a, r, out _, out _, out var endPointX, out var endPointY);

                    if (DoubleStatic.AreClose(360, a))
                    {
                        endPointX = r;
                        endPointY = 0;
                    }

                    _data.Parts[i] = new Part
                    {
                        Start   = _data.Parts[i - 1].End,
                        End     = new Point(endPointX + half, endPointY + half),
                        IsLarge = endAngle >= 180,
                        IsClose = endAngle >= 359d,
                        Value   = v,
                        Color   = series.Color,
                        Percent = percent,
                    };
                }
            }

        }

        
        private Brush GetBrush(int i, ChartPalette palette)
        {
            if (Values.Count == 0)
            {
                return null;
            }

            var color = Values[i].Color;

            if (string.IsNullOrEmpty(color))
            {
                return palette.GetBrush(i);
            }
            
            return new SolidColorBrush(Xaml.ToColor(color));
        }

        private void Draw(DrawingContext dc)
        {
            if (_data is null) return;

            var palette   = Palette ?? ChartPalette.CreateRainbow();
            var thickness = Math.Clamp(StrokeThickness, 2, 512);
            
            //var bgPen     = new Pen(Background ?? new SolidColorBrush(Colors.Gray), thickness);

            //
            //
            dc.DrawRectangle(TransparentBrush, null, new Rect(0, 0, _data.Width, _data.Height));

            //
            //
            // dc.DrawEllipse(null, bgPen, _data.Center, _data.Radius, _data.Radius);

            for (var i = 0; i < _data.Parts.Length; i++)
            {
                var part = _data.Parts[i];

                if (DoubleStatic.AreClose(0, part.Percent))
                {
                    continue;
                }

                var brush = string.IsNullOrEmpty(part.Color) ? GetBrush(i, palette) : Xaml.ToBrush(part.Color);
                var fgPen = CreateCapStylePen(brush, thickness);

                if (DoubleStatic.AreClose(part.Percent ,1d))
                {
                    dc.DrawEllipse(null, fgPen, _data.Center, _data.Radius, _data.Radius);
                }
                else
                {
                    var geometry = CreateGeometry(_data.Radius, part.IsLarge, part.IsClose, part.Start, part.End);
                    dc.DrawGeometry(null, fgPen, geometry);
                }

                //dc.DrawEllipse(brush, null, part.End, 2, 2);
                //if (i == 0) dc.DrawEllipse(brush, null, part.Start, 2, 2);
            }
            //
            //

            //
            // Debug
        }

        /// <summary>
        /// 渲染数据
        /// </summary>
        public RenderData DrawingInfo => _data;
    }
}
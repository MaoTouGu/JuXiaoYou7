
namespace KinonekoSoftware.UI.Charts
{
    public partial class RingControl : ChartControl
    {
        public class RenderData
        {
            public double Diameter { get; internal set; }
            public double Radius   { get; internal set; }
            public double Width    { get; internal set; }
            public double Height   { get; internal set; }
            public Point  Start    { get; internal set; }
            public Point  End      { get; internal set; }
            public Point  Center   { get; internal set; }
            public double Percent  { get; internal set; }
            public double Value    { get; internal set; }
            public bool   IsClose  { get; internal set; }
            public bool   IsLarge  { get; internal set; }
        }

        private RenderData _data;

        private partial PathGeometry CreateGeometry(double radius, bool isLarge, bool isClose, Point start, Point end);

        protected void Prepare()
        {

            GetSize(out var w, out var h);

            var half   = Math.Clamp(StrokeThickness, 2, 512);
            var d      = Math.Min(w, h);
            var r      = d / 2d - half;
            var center = new Point(r + half, r + half);
            var min    = Minimum;
            var max    = Maximum;

            if (min > max)
            {
                (min, max) = (max, min);
            }

            var v        = Math.Clamp(Value, min, max);
            var percent  = v       / max;
            var endAngle = percent * 359d;

            _data = new RenderData
            {
                Diameter = d,
                Percent  = percent,
                Value    = v,
                Center   = center,
                Width    = w,
                Height   = h,
                Radius   = r,
            };

            ArcAssist.GetArcSegments(endAngle, r, out var isLarge, out var isClose, out var endPointX, out var endPointY);
            CircularAssits.GetPointFromAngle(0, r, out var startPointX, out var startPointY);

            _data.Start   = new Point(startPointX + half, startPointY + half);
            _data.End     = new Point(endPointX   + half, endPointY   + half);
            _data.IsLarge = isLarge;
            _data.IsClose = isClose;
        }


        private void Draw(DrawingContext dc)
        {
            if (_data is null) return;
            var brush     = Stroke ?? new SolidColorBrush(Colors.Olive);
            var thickness = Math.Clamp(StrokeThickness, 2, 512);
            var fgPen     = CreateCapStylePen(brush, thickness);
            var bgPen     = new Pen(Background ?? new SolidColorBrush(Colors.Gray), thickness);
            var geometry  = CreateGeometry(_data.Radius, _data.IsLarge, _data.IsClose, _data.Start, _data.End);

            //
            //
            dc.DrawRectangle(TransparentBrush, null, new Rect(0, 0, _data.Width, _data.Height));

            //
            //
            dc.DrawEllipse(null, bgPen, _data.Center, _data.Radius, _data.Radius);


            //
            //
            dc.DrawGeometry(null, fgPen, geometry);

            //
            // Debug
            // var debugger  = new SolidColorBrush(Colors.CadetBlue);
            // dc.DrawEllipse(debugger, null, _data.End, 12, 12);
            // dc.DrawEllipse(debugger, null, _data.Start, 12, 12);
        }

        /// <summary>
        /// 渲染数据
        /// </summary>
        public RenderData DrawingInfo => _data;
    }
}
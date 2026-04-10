using System.Diagnostics;

namespace KinonekoSoftware.UI.Charts
{
    /// <summary>
    /// 表示一个雷达插件
    /// </summary>
    public partial class RadarControl : ChartControl
    {
        private       DateTime   _lastDrawingTimestamp = DateTime.Now;
        private       RenderData _data;
        private const double     _offset = 270d;

        public class RenderData
        {

            /// <summary>
            /// 画刷
            /// </summary>
            public Brush Brush { get;             internal set; }
            public Point       CenterPoint { get; internal set; }
            public double      Radius      { get; internal set; }
            public int         AxisCount   { get; internal set; }
            public ChartData[] Values      { get; internal set; }
            public AxisData[]  Axes        { get; internal set; }
            public double      W           { get; internal set; }
            public double      H           { get; internal set; }
        }

        public class ChartData
        {
            public double[] ValuesInPercent { get; internal set; }
            public Point[]  ValueInPoints   { get; internal set; }
        }

        public class AxisData
        {
            public double Angle { get; internal set; }
            /// <summary>
            /// 半径
            /// </summary>
            public double Radius { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public Point[] Points { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public ChartAxis Axis { get; internal set; }
        }

        protected void Prepare()
        {

            var values     = Values ?? new ChartSeriesCollection();
            var valueCount = values.Count;
            var axisCount = values.Count == 0 ?
                0 :
                values.Select(sc => sc.Count)
                      .Prepend(int.MaxValue)
                      .Min();
            if (axisCount == 0)
            {
                return;
            }
            var avgAngle = 360d / axisCount;
            var palette  = Palette ?? ChartPalette.Create(Colors.DarkGray, Colors.CornflowerBlue);

            //
            // 计算半径和中心点
            CalculateRadius(4, out var d, out var r, out var center);

            _data = new RenderData
            {
                Axes        = new AxisData[axisCount],
                Values      = new ChartData[valueCount],
                AxisCount   = axisCount,
                CenterPoint = center,
                Radius      = r,
                Brush       = palette.Stroke,
                W           = d,
                H           = d,
            };

            //
            // 计算标尺
            var angle = _offset;
            PrepareAxisCollection(axisCount, r, avgAngle, angle, ref center);

            //
            // 计算值
            angle = _offset;
            for (var i = 0; i < values.Count; i++)
            {
                var collection = values[i];
                var v = new ChartData
                {
                    ValuesInPercent = new double[collection.Count],
                    ValueInPoints   = new Point[collection.Count],
                };

                //
                //
                _data.Values[i] = v;

                //
                //
                PrepareSeriesCollection(i, r, avgAngle, ref angle, ref center, collection);
            }
        }

        private void PrepareAxisCollection(int axisCount, double r, double avgAngle, double angle, ref Point center)
        {
            for (var i = 0; i < axisCount; i++)
            {

                //
                // 计算边界线
                var rules = GetRulePointsFromAngle(angle, r, ref center);
                var axes  = Axes[i];

                var axis = new AxisData
                {
                    Angle = angle,
                    Radius = r,
                    Axis   = axes,
                    Points = rules,
                };


                _data.Axes[i] = axis;
                axes.X        = rules[0].X;
                axes.Y        = rules[0].Y;
                angle         = (avgAngle + angle) % 360;
            }
        }

        protected void PrepareSeriesCollection(int i, double r, double avgAngle, ref double angle, ref Point center, ChartSeries collection)
        {
            var v = new ChartData
            {
                ValuesInPercent = new double[collection.Count],
                ValueInPoints   = new Point[collection.Count],
            };

            //
            //
            _data.Values[i] = v;

            //
            //
            for (var n = 0; n < collection.Count; n++)
            {
                var series  = collection[n];
                var percent = Math.Clamp(Math.Max(series.Value, collection.Minimum) / collection.Maximum, 0, 1);
                var p       = GetPointFromAngle(angle, percent                      * r, ref center);
                //
                //
                v.ValueInPoints[n]   = p;
                v.ValuesInPercent[n] = percent;
                angle                = (avgAngle + angle) % 360;
            }
        }

        private void Draw(DrawingContext dc)
        {
            if (_data is null ||
                _data.AxisCount == 0)
            {
                return;
            }

            //
            //
            dc.DrawRectangle(Background ?? TransparentBrush, null, new Rect(0, 0, _data.W, _data.H));

            //
            //
            DrawAxis(dc);

            //
            //
            DrawValue(dc);
        }

        // ReSharper disable RedundantArgumentDefaultValue
        private void DrawAxis(DrawingContext dc)
        {
            var count = _data.AxisCount;
            var outer = new Pen(_data.Brush, 2);
            var inner = new Pen(_data.Brush, 1)
            {
                DashStyle = DashStyles.Dash,
            };

            if (ConnectionLineKind == ConnectionLineKind.Line)
            {
                for (var i = 0; i < count; i++)
                {
                    var axis     = _data.Axes[i];
                    var nextAxis = _data.Axes[(i + 1) % count];
                    dc.DrawLine(inner, _data.CenterPoint, axis.Points[0]);
                    dc.DrawLine(outer, axis.Points[0], nextAxis.Points[0]);
                    dc.DrawLine(inner, axis.Points[1], nextAxis.Points[1]);
                    dc.DrawLine(inner, axis.Points[2], nextAxis.Points[2]);
                    dc.DrawLine(inner, axis.Points[3], nextAxis.Points[3]);
                    dc.DrawLine(inner, axis.Points[4], nextAxis.Points[4]);
                }
            }
            else
            {
                for (var i = 0; i < count; i++)
                {
                    var axis = _data.Axes[i];
                    dc.DrawLine(inner, _data.CenterPoint, axis.Points[0]);
                }
                dc.DrawEllipse(null, outer, _data.CenterPoint, _data.Radius, _data.Radius);
                dc.DrawEllipse(null, inner, _data.CenterPoint, _data.Radius * 0.8d, _data.Radius * 0.8d);
                dc.DrawEllipse(null, inner, _data.CenterPoint, _data.Radius * 0.6d, _data.Radius * 0.6d);
                dc.DrawEllipse(null, inner, _data.CenterPoint, _data.Radius * 0.4d, _data.Radius * 0.4d);
                dc.DrawEllipse(null, inner, _data.CenterPoint, _data.Radius * 0.2d, _data.Radius * 0.2d);
            }
            
            if(count is > 0 and < 19)
            {
                DrawAxis(dc, _data.Axes, Axes);
            }
        }

        private void DrawValue(DrawingContext dc)
        {
            var valueLength = _data.Values.Length;
            var palette     = Palette ?? ChartPalette.Create(Colors.DarkGray, Colors.Olive);

            for (var i = 0; i < valueLength; i++)
            {
                var seriesData = _data.Values[i];
                var stroke     = palette.GetBrush(i);
                var fill       = palette.GetBrush(i, true);
                var length     = seriesData.ValueInPoints.Length;
                var pen        = new Pen(stroke, 2);

                if (length <= 2)
                {
                    //
                    // 小于两条轴不绘制
                    continue;
                }


                for (var n = 0; n < length; n++)
                {
                    var lastIndex = n == 0 ? length - 1 : n - 1;
                    var nextIndex = (n + 1) % length;

                    if (DoubleUtils.AreClose(0, seriesData.ValuesInPercent[lastIndex]) &&
                        DoubleUtils.AreClose(0, seriesData.ValuesInPercent[nextIndex]))
                    {
                        dc.DrawLine(pen, _data.CenterPoint, seriesData.ValueInPoints[n]);
                        dc.DrawEllipse(stroke, null, seriesData.ValueInPoints[n], 4, 4);
                    }
                    else
                    {
                        var geometry = CreateTriangleGeometry(_data.CenterPoint, seriesData.ValueInPoints[n], seriesData.ValueInPoints[nextIndex]);
                        dc.DrawGeometry(fill, null, geometry);
                        // dc.DrawEllipse(new SolidColorBrush(Colors.SlateGray), null, seriesData.ValueInPoints[n], 8, 8);
                    }
                }
            }
        }


        /// <summary>
        /// 渲染数据
        /// </summary>
        public RenderData DrawingInfo => _data;
    }
}
using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace KinonekoSoftware.UI.Geometries
{
    public static class ArcAssist
    {
        public const double Threshold_Angle = 359d;
        public const double Threshold_Arc = 180d;
        

        /// <summary>
        /// 获得弧线段的参数
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="radius">半径（需要处理过）</param>
        /// <param name="isLarge">输出，是否是大弧线</param>
        /// <param name="isClose">输出，是否为闭合曲线</param>
        /// <param name="endPointX">输出，弧线末端X坐标</param>
        /// <param name="endPointY">输出，弧线末端Y坐标</param>
        /// <remarks>
        /// 注意，radius 需要提前处理过轮廓的影响。
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetArcSegments(
            double     angle,
            double     radius,
            out bool   isLarge,
            out bool   isClose,
            out double endPointX,
            out double endPointY)
        {
            //
            // 判断哪个才是最小的角度。
            angle = Math.Min(angle, Threshold_Angle);

            //
            //
            var radian = CircularAssits.Angle2Radian(angle);
            var a      = radius * Math.Sin(radian);
            var b      = radius * Math.Cos(radian);
            isLarge   = angle >= Threshold_Arc;
            isClose   = angle >= Threshold_Angle;
            endPointX = radius + a;
            endPointY = radius - b;
        }

        /// <summary>
        /// 获得弧线段的参数
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="radius">半径（需要处理过）</param>
        /// <param name="isLarge">输出，是否是大弧线</param>
        /// <param name="isClose">输出，是否为闭合曲线</param>
        /// <remarks>
        /// 注意，radius 需要提前处理过轮廓的影响。
        /// </remarks>
        /// <returns>返回一个坐标</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point GetArcSegments(double angle, double radius, out bool isLarge, out bool isClose)
        {
            //
            // 判断哪个才是最小的角度。
            angle = Math.Min(angle, Threshold_Angle);

            //
            //
            var radian = CircularAssits.Angle2Radian(angle);
            var a      = radius * Math.Sin(radian);
            var b      = radius * Math.Cos(radian);
            isLarge = angle >= Threshold_Arc;
            isClose = angle >= Threshold_Angle;
            return new Point(radius + a, radius - b);
        }
        
        /// <summary>
        /// 获得弧线段的参数
        /// </summary>
        /// <param name="angle">角度</param>
        /// <param name="radius">半径（需要处理过）</param>
        /// <param name="isLarge">输出，是否是大弧线</param>
        /// <param name="isClose">输出，是否为闭合曲线</param>
        /// <remarks>
        /// 注意，radius 需要提前处理过轮廓的影响。
        /// </remarks>
        /// <returns>返回一个坐标</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point GetPoint(double angle, double radius)
        {
            //
            // 判断哪个才是最小的角度。
            angle = Math.Min(angle, Threshold_Angle);

            //
            //
            var radian = CircularAssits.Angle2Radian(angle);
            var a      = radius * Math.Sin(radian);
            var b      = radius * Math.Cos(radian);
            
            //
            //
            return new Point(radius + a, radius - b);
        }

        /// <summary>
        /// 创建一个弧形 <see cref="Geometry"/>。
        /// </summary>
        /// <param name="r">圆弧的半径</param>
        /// <param name="offset">起始角度</param>
        /// <param name="angle">值，大小为:0~360</param>
        public static Geometry Create(double r, double offset, double angle)
        {
            offset %= 360;
            angle  %= 360;


            var close = angle >= Threshold_Angle;
            var large = angle >= Threshold_Arc;
            var size  = new Size(r, r);
            var start = GetPoint(offset, r);
            var end   = GetPoint(angle, r);
            var segment = new ArcSegment
            {
                IsLargeArc     = large,
                Size           = size,
                Point          = end,
                SweepDirection = offset <= angle ? SweepDirection.Clockwise : SweepDirection.CounterClockwise,
            };

            var geometry = new PathGeometry
            {
                Figures = new PathFigures()
                {
                    new PathFigure
                    {
                        StartPoint = start,
                        IsClosed   = close,
                        Segments = new PathSegments
                        {
                            segment,
                        },
                    },
                },
            };
            return geometry;
        }


        /// <summary>
        /// 创建一个弧形 <see cref="Geometry"/>。
        /// </summary>
        /// <param name="r">圆弧的半径</param>
        /// <param name="start">起始坐标</param>
        /// <param name="end">结束坐标</param>
        /// <param name="angle">值，大小为:0~360</param>
        public static Geometry Create(double r, Point start, Point end, double angle)
        {
            angle %= 360;


            var close = angle >= Threshold_Angle;
            var large = angle >= Threshold_Arc;
            var size  = new Size(r, r);
            var segment = new ArcSegment
            {
                IsLargeArc     = large,
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
                        IsClosed   = close,
                        Segments = new PathSegments
                        {
                            segment,
                        },
                    },
                },
            };
            return geometry;
        }
    }
}
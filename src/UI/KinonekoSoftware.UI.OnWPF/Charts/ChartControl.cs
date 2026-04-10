using System.Runtime.CompilerServices;



namespace KinonekoSoftware.UI.Charts
{
    public class ChartControlSelection
    {
        public ChartAxis Axis { get; init; }
        public Series Series { get; init; }
        public ChartSeries ChartSeries { get; init; }
        public int IndexInChartSeries { get; init; }
        public int IndexInChartSeriesCollection { get; init; }
    }
    
    public abstract partial class ChartControl : Control
    {
        public static partial PathGeometry CreatePathGeometry(Point[] points);
        public static partial PathGeometry CreateTriangleGeometry(Point center, Point p1, Point p2);

        public static double GetAngleFromPoint(double x, double y)
        {
            var a = Math.Atan2(y, x);
            if (a > -Math.PI && a < 0)
            {
                a += 2 * Math.PI;
            }

            return Radian2Angle(a);
        }

        public static double GetLength(double x, double y) => Math.Sqrt(x * x + y * y);
        
        /// <summary>
        /// 创建坐标点。
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>返回一个指定的坐标点</returns>
        public static partial Point CreatePoint(double x, double y);
        
        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="amend">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="diameter">计算得到的直径</param>
        /// <param name="radius">计算得到的半径</param>
        /// <param name="centerPoint">计算得到的中心点</param>
        protected partial void CalculateRadius(int amend, out double diameter, out double radius, out Point centerPoint);


        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="amend">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="diameter">计算得到的直径</param>
        /// <param name="radius">计算得到的半径</param>
        /// <param name="centerPoint">计算得到的中心点</param>
        public static partial void CalculateRadius(FrameworkElement control, int amend, out double diameter, out double radius, out Point centerPoint);

        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="w">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="h">计算得到的直径</param>
        protected partial void GetSize(out double w, out double h);

        /// <summary>
        /// 获得指定角度的所有刻度尺坐标
        /// </summary>
        /// <param name="angle">指定要获得的角度</param>
        /// <param name="r">半径</param>
        /// <param name="centerPoint">中心点</param>
        public static Point[] GetRulePointsFromAngle(double angle, double r, ref Point centerPoint)
        {
            var radian  = Angle2Radian(angle);
            var percent = 1.0d;
            var p       = new Point[5];
            for (var i = 0; i < 5; i++)
            {
                p[i] = CreatePoint(
                    x: r * percent * Math.Cos(radian) + centerPoint.X,
                    y: r * percent * Math.Sin(radian) + centerPoint.Y);

                percent -= 0.2d;
            }

            return p;
        }


        /// <summary>
        /// 获得指定角度的坐标
        /// </summary>
        /// <param name="angle">指定要获得的角度</param>
        /// <param name="r">半径</param>
        /// <param name="centerPoint">中心点</param>
        public static Point GetPointFromAngle(double angle, double r, ref Point centerPoint)
        {
            var radian = Angle2Radian(angle);
            return CreatePoint(
                x: r * Math.Cos(radian) + centerPoint.X,
                y: r * Math.Sin(radian) + centerPoint.Y);
        }


        /// <summary>
        /// 弧度制转角度值
        /// </summary>
        /// <param name="radius">弧度制</param>
        /// <returns>角度值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Radian2Angle(double radius) => radius / Math.PI * 180d;

        /// <summary>
        /// 角度值转弧度制
        /// </summary>
        /// <param name="angle">角度值</param>
        /// <returns>弧度制</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle2Radian(double angle) => angle / 180d * Math.PI;
    }
}
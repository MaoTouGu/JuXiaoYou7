using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MaoTouGu.Foundation.Mathematics
{
    public sealed class Angle
    {
        public const double Threshold_Angle = 359d;

        private double _angle;
        private double _radian;

        public Angle()
        {
            Change(0d);
        }

        public Angle(double radian)
        {
            Change(radian);
        }

        internal void Change(double radian)
        {
            var angle = radian * 180 / Math.PI;

            //
            //
            _radian = radian;
            _angle  = angle < 0 ? -angle : 360 - angle;
        }

        public void GetPosition(Radius r, out double x, out double y)
        {
            // var a = r.Value * Math.Sin(_radian);
            // var b = r.Value * Math.Cos(_radian);
            //
            // x = r.Value + a;
            // y = r.Value - b;

            x = r.Value * Math.Cos(_radian) + r.CenterX;
            y = r.Value * Math.Sin(_radian) + r.CenterY;
        }

        /// <summary>
        /// 弧度转角度。
        /// </summary>
        /// <param name="radius">0~1的弧度值</param>
        /// <returns>返回0~359°的角度值。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Radian2Angle(double radius) => radius / Math.PI * 180d;

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="angle">0~359°的角度值</param>
        /// <returns>返回0~1的弧度值。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle2Radian(double angle) => angle / 180d * Math.PI;

        public static void GetPosition(double r, double angle, out double x, out double y)
        {

            //
            // 判断哪个才是最小的角度。
            angle = Math.Min(angle, Threshold_Angle);

            //
            //
            var radian = Angle2Radian(angle);
            var a      = r * Math.Sin(radian);
            var b      = r * Math.Cos(radian);

            x = r + a;
            y = r - b;
        }

        public static void GetPosition(double centerX, double centerY, double r, double angle, out double x, out double y)
        {

            //
            // 判断哪个才是最小的角度。
            angle = Math.Min(angle, Threshold_Angle);

            //
            //
            var radian = Angle2Radian(angle);
            var a      = r * Math.Sin(radian);
            var b      = r * Math.Cos(radian);

            x = r     + a + centerX;
            y = r - b + centerY;
        }

        public double Radian => _radian;
        public double Value  => _angle;

        public override string ToString() => _angle.ToString(CultureInfo.CurrentCulture);
    }
}
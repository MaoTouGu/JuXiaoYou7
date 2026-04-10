using System.Runtime.CompilerServices;

namespace KinonekoSoftware.UI
{
    public class CircularAssits
    {
        /// <summary>
        /// 弧度转角度。
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Radian2Angle(double radius) => radius / Math.PI * 180d;

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle2Radian(double angle) => angle / 180d * Math.PI;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="r"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void GetPointFromAngle(double angle, double r, out double x, out double y)
        {
            var radian = Angle2Radian(angle);
            x = r * Math.Cos(radian);
            y = r * Math.Sin(radian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="r"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void GetPointFromAngle(double angle, double r, double centerX, double centerY, out double x, out double y)
        {
            var radian = Angle2Radian(angle);
            x = r * Math.Cos(radian) + centerX;
            y = r * Math.Sin(radian) + centerY;
        }
    }
}
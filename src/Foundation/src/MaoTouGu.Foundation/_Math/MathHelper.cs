using System.Runtime.CompilerServices;

namespace MaoTouGu.Foundation.Mathematics
{
    public static class MathHelper
    {
        /// <summary>
        /// 角度值变弧度值
        /// </summary>
        /// <param name="angle">角度值，值范围为[0-359]</param>
        /// <returns>返回弧度值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleToRadian(double angle)
        {
            return angle * Math.PI / 180d;   
        }

        /// <summary>
        /// 弧度值变角度值
        /// </summary>
        /// <param name="radian"></param>
        /// <returns>返回角度值，值范围为[0-359]</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RadianToAngle(double radian)
        {
            return radian * 180d / Math.PI;
        }
    }
}
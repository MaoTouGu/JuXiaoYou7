namespace MaoTouGu.Foundation.Mathematics
{
    public static class CoordinateSystem
    {
        public static double GetAngle(double x, double y)
        {
            var angle = Math.Atan2(y, x) * 180 / Math.PI;
            return angle < 0 ? -angle : 360 - angle;
        }
        
        /// <summary>
        /// 给定弧度制，获得指定角度的坐标。
        /// </summary>
        /// <param name="r">给定的半径。</param>
        /// <param name="radian">弧度制，范围为0~1。</param>
        /// <param name="x">返回的新坐标X轴。</param>
        /// <param name="y">返回的新坐标Y轴。</param>
        public static void CirculationPositionConstraint(double r, double radian, out double x, out double y)
        {
            x = r * Math.Cos(radian);
            y = r * Math.Sin(radian);
        }
        
        /// <summary>
        /// 给定弧度制，获得指定角度的坐标。
        /// </summary>
        /// <param name="r">给定的半径。</param>
        /// <param name="radian">弧度制，范围为0~1。</param>
        /// <param name="centerX">给定的中心坐标X轴。</param>
        /// <param name="centerY">给定的中心坐标Y轴。</param>
        /// <param name="x">返回的新坐标X轴。</param>
        /// <param name="y">返回的新坐标Y轴。</param>
        public static void CirculationPositionConstraint(double r, double radian, double centerX, double centerY, out double x, out double y)
        {
            x = r * Math.Cos(radian) + centerX;
            y = r * Math.Sin(radian) + centerY;
        }

        /// <summary>
        /// 给定位置、半径，完成坐标的圆形位置约束。
        /// </summary>
        /// <param name="inputX">给定的X坐标。</param>
        /// <param name="inputY">给定的Y坐标。</param>
        /// <param name="centerX">给定的中心坐标X轴。</param>
        /// <param name="centerY">给定的中心坐标Y轴。</param>
        /// <param name="r">给定的半径。</param>
        /// <param name="x">返回的新坐标X轴。</param>
        /// <param name="y">返回的新坐标Y轴。</param>
        public static void CirculationPositionConstraint(double inputX, double inputY, double centerX, double centerY, double r, out double x, out double y)
        {
            var radian = Math.Atan2(inputY - centerY, inputX - centerX);
            x = r * Math.Cos(radian) + centerX;
            y = r * Math.Sin(radian) + centerY;
        }
        
        /// <summary>
        /// 给定位置、半径，完成坐标的圆形位置约束。
        /// </summary>
        /// <param name="inputX">给定的X坐标。</param>
        /// <param name="inputY">给定的Y坐标。</param>
        /// <param name="centerX">给定的中心坐标X轴。</param>
        /// <param name="centerY">给定的中心坐标Y轴。</param>
        /// <param name="r">给定的半径。</param>
        /// <param name="x">返回的新坐标X轴。</param>
        /// <param name="y">返回的新坐标Y轴。</param>
        /// <param name="angle">返回的角度值。</param>
        public static void CirculationPositionConstraint(
            double inputX,
            double inputY, 
            double centerX,
            double centerY, 
            double r, 
            out double x,
            out double y,
            out double angle)
        {
            var radian = Math.Atan2(inputY - centerY, inputX - centerX);
            x     = r      * Math.Cos(radian) + centerX;
            y     = r      * Math.Sin(radian) + centerY;
            angle = radian * 180 / Math.PI;
            angle = angle < 0 ? -angle : 360 - angle;
        }
    }
}
// ----------------------------------------------------------
//            文件：CornerRadiusConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class CornerRadiusConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i32T = value is Int32CornerRadius n ? n : new Int32CornerRadius();
            var t = new CornerRadius
            {
                TopLeft     = i32T.LeftTop,
                TopRight    = i32T.RightTop,
                BottomLeft  = i32T.LeftBottom,
                BottomRight = i32T.RightBottom,
            };
            return t;
        }
    }
}
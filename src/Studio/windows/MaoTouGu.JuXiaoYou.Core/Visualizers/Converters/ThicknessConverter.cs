// ----------------------------------------------------------
//            文件：ThicknessConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class ThicknessConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i32T = value is Int32Thickness n ? n : new Int32Thickness();
            var t = new Thickness
            {
                Left   = i32T.Left,
                Right  = i32T.Right,
                Top    = i32T.Top,
                Bottom = i32T.Bottom,
            };
            return t;
        }
    }
}
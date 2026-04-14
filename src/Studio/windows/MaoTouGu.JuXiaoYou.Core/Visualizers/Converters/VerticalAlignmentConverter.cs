// ----------------------------------------------------------
//            文件：VerticalAlignmentConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class VerticalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var n = value is int i ? i : 0;

            return n switch
            {
                1 => VerticalAlignment.Center,
                2 => VerticalAlignment.Bottom,
                3 => HorizontalAlignment.Stretch,
                _ => VerticalAlignment.Top,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var align = value is VerticalAlignment t ? t : VerticalAlignment.Stretch;

            return align switch
            {
                VerticalAlignment.Center  => 1,
                VerticalAlignment.Bottom  => 2,
                VerticalAlignment.Stretch => 3,
                _                         => 0,
            };
        }
    }
}
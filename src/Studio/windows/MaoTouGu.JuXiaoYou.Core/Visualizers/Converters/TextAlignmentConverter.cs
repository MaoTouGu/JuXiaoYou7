// ----------------------------------------------------------
//            文件：TextAlignmentConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class TextAlignmentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var n = value is int i ? i : 0;

            return n switch
            {
                1 => TextAlignment.Center,
                2 => TextAlignment.Right,
                3 => TextAlignment.Justify,
                _ => TextAlignment.Left,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var align = value is TextAlignment t ? t : TextAlignment.Left;

            return align switch
            {
                TextAlignment.Center  => 1,
                TextAlignment.Right   => 2,
                TextAlignment.Justify => 3,
                _                     => 0,
            };
        }
    }
}
// ----------------------------------------------------------
//            文件：HorizontalAlignmentConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class HorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var n = value is int i ? i : 0;

            return n switch
            {
                1 => HorizontalAlignment.Center,
                2 => HorizontalAlignment.Right,
                3 => HorizontalAlignment.Stretch,
                _ => HorizontalAlignment.Left,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var align = value is HorizontalAlignment t ? t : HorizontalAlignment.Stretch;

            return align switch
            {
                HorizontalAlignment.Center  => 1,
                HorizontalAlignment.Right   => 2,
                HorizontalAlignment.Stretch => 3,
                _                           => 0,
            };
        }
    }
}
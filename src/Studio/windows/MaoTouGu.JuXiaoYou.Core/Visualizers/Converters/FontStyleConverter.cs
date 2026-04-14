// ----------------------------------------------------------
//            文件：FontStyleConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class FontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var r = value is bool b && b;

            return r ? FontStyles.Italic : FontStyles.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FontStyle s)
            {
                return s == FontStyles.Italic;
            }

            return Boxing.False;
        }
    }
}
// ----------------------------------------------------------
//            文件：UrlConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月24日 14:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public sealed class UrlConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value?.ToString();

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (str.Length < 10)
            {
                return str;
            }

            var prefix = str.Substring(0, 4);
            var suffix = str.Substring(str.Length   - 4, 4);
            var mask   = new string('*', str.Length - prefix.Length - suffix.Length);
            return $"{prefix}{mask}{suffix}";
        }
    }
}
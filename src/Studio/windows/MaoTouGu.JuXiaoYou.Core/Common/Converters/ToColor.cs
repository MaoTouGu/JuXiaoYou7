// ----------------------------------------------------------
//            文件：ToColor.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:18
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Common.Converters
{
    public class ToColor : OneWayConverter
    {
        private const double One = 1d;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var opacity = parameter is double d ? d : One;
            var str     = value?.ToString();

            var c = Xaml.ToColor(str);

            c.A = (byte)(opacity * 255);

            return c;
        }
    }

    public class ToBrush : OneWayConverter
    {
        private const double One = 1d;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var opacity = parameter is double d ? d : One;
            var str     = value?.ToString();

            var c = Xaml.ToColor(str);
            var b = new SolidColorBrush(c);

            b.Opacity = opacity;

            return b;
        }
    }
}
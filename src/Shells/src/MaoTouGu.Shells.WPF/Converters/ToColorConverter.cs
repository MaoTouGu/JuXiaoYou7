// ----------------------------------------------------------
//            文件：ToColorConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月19日 19:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Converters
{
    public class ToColorConverter : IValueConverter
    {
        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = Xaml.ToColor(value?.ToString());

            b.A = (byte)(GetOpacity(parameter) * 255);

            return b;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color c)
            {
                return $"#{c.R:x}{c.G:x}{c.B:x}".ToUpper();
            }

            return "#000000";
        }

        static double GetOpacity(object parameter)
        {
            if (parameter is double d)
            {
                return Math.Clamp(d, 0d, 1d);
            }

            if (parameter is float f)
            {
                return Math.Clamp(f, 0d, 1d);
            }

            return 1d;
        }
    }
}
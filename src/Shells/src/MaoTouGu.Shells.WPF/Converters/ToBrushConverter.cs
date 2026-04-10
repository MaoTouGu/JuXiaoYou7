using System.Globalization;
using System.Windows.Data;

namespace MaoTouGu.Shells.Converters
{
    public class ToBrushConverter : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = Xaml.ToBrush(value?.ToString());

            b.Opacity = GetOpacity(parameter);

            return b;
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
using System.Globalization;
using Avalonia.Data.Converters;

namespace KinonekoSoftware.UI.Converters
{
    public abstract class OneWayConverter : IValueConverter
    {

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
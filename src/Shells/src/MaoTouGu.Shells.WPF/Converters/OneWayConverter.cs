
namespace MaoTouGu.Shells.Converters
{
    public abstract class OneWayConverter : IValueConverter
    {

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
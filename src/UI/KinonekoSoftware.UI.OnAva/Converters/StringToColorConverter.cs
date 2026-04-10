

using System.Globalization;
using Avalonia.Data.Converters;
using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Converters
{
    public sealed class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Xaml.ToColor(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
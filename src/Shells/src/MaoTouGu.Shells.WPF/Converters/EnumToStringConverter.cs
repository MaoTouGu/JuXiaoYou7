using MaoTouGu.Shells.Languages;

namespace MaoTouGu.Shells.Converters
{
    public class EnumToStringConverter : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return I18N.GetEnum(value as Enum);
        }

    }
}
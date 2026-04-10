namespace KinonekoSoftware.UI.Converters
{
    public class EnumEqualityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || parameter is null)
            {
                return false;
            }

            if (value is Enum && parameter is Enum && value.GetType() == parameter.GetType())
            {
                var a = System.Convert.ChangeType(value, typeof(int));
                var b = System.Convert.ChangeType(parameter, typeof(int));

                return ((int)a) == ((int)b);
            }

            if (value.GetType() == parameter.GetType())
            {
                return value == parameter;
            }

            return false;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
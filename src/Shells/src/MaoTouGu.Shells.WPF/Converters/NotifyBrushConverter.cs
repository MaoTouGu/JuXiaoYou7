namespace MaoTouGu.Shells.Converters
{
    internal class NotifyBrushConverter : OneWayConverter
    {
                
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not NotifyType type)
            {
                type = NotifyType.SlateGray;
            }

            var key = type switch
            {
                NotifyType.Danger    => "Brush.Error100",
                NotifyType.Warning   => "Brush.Warning100",
                NotifyType.SlateGray => "Brush.SlateGray100",
                NotifyType.Success   => "Brush.Success100",
                NotifyType.Obsoleted => "Brush.Obsoleted100",
                _                    => "Brush.Info100",
            };

            return Xaml.Find(key);
        }
    }
}
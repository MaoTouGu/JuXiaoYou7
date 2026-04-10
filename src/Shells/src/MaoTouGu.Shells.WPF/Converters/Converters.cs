namespace MaoTouGu.Shells.Converters
{
    /// <summary>
    /// <see cref="Converters"/> 类型用于维护所有转换器。
    /// </summary>
    public static class Converters
    {

        public static OneWayConverter IsNull    { get; } = new NullToBooleanConverter(true);
        public static OneWayConverter IsNotNull { get; } = new NullToBooleanConverter(false);
        
        public static ToBrushConverter      ToBrush      { get; } = new ToBrushConverter();
        public static ToColorConverter      ToColor      { get; } = new ToColorConverter();
        public static OneWayConverter       NotifyBrush  { get; } = new NotifyBrushConverter();
        public static EnumToStringConverter EnumToString { get; } = new EnumToStringConverter();

        public static ToVisibilityConverter        TrueToVisibility    { get; } = new ToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);
        public static ToVisibilityConverter        FalseToVisibility   { get; } = new ToVisibilityConverter(Visibility.Collapsed, Visibility.Visible);
        public static ZeroToVisibilityConverter    ZeroToVisibility    { get; } = new ZeroToVisibilityConverter();
        public static NotZeroToVisibilityConverter NotZeroToVisibility { get; } = new NotZeroToVisibilityConverter();
    }
}



namespace MaoTouGu.Editor.Formatters
{
    public class DefaultTextRunProperties: GenericTextRunProperties
    {
        public DefaultTextRunProperties(DpiScale scale) : base(scale.PixelsPerDip)
        {
            SetForeground(new SolidColorBrush(Colors.Black));
            SetFontSize(14);
            SetTypeface(new FontFamily("Microsoft Yahei"));
        }
        
        public DefaultTextRunProperties(double pixelsPerDip) : base(pixelsPerDip)
        {
            SetForeground(new SolidColorBrush(Colors.Black));
            SetFontSize(14);
            SetTypeface(new FontFamily("Microsoft Yahei"));
        }
    }
}
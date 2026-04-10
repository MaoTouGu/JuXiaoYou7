namespace KinonekoSoftware.UI.Controls
{
    public class MultiLine: TextBoxBase
    {
        
    }
    

    public sealed class HeaderedMultiLine : MultiLine
    {
        public static readonly StyledProperty<string> HeaderProperty = AvaloniaProperty.Register<HeaderedMultiLine, string>(nameof(Header));

        public string Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
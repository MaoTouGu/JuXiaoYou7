namespace KinonekoSoftware.UI.Controls
{
    public class MultiLine: TextBoxBase
    {
    }
    
    public sealed class HeaderedMultiLine : MultiLine
    {
        
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
                                                                                               nameof(Header),
                                                                                               typeof(string),
                                                                                               typeof(HeaderedMultiLine),
                                                                                               new PropertyMetadata(default(string)));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
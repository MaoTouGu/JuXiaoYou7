namespace KinonekoSoftware.UI.Controls
{
    public abstract class TextBoxBase : TextBox
    {
        public static readonly DependencyPropertyKey HasTextPropertyKey;
        public static readonly DependencyProperty    HasTextProperty;
        public static readonly DependencyProperty    WatermarkProperty;
        public static readonly DependencyProperty    CornerRadiusProperty;

        static TextBoxBase()
        {
            CornerRadiusProperty = DependencyProperty.Register(
                                                               nameof(CornerRadius),
                                                               typeof(CornerRadius),
                                                               typeof(TextBoxBase),
                                                               new PropertyMetadata(default(CornerRadius)));

            WatermarkProperty = DependencyProperty.Register(
                                                            nameof(Watermark),
                                                            typeof(string),
                                                            typeof(TextBoxBase),
                                                            new PropertyMetadata(default(string)));

            HasTextPropertyKey = DependencyProperty.RegisterReadOnly(
                                                                     nameof(HasText),
                                                                     typeof(bool),
                                                                     typeof(TextBoxBase),
                                                                     new PropertyMetadata(default(bool)));

            HasTextProperty = HasTextPropertyKey.DependencyProperty;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            HasText = !string.IsNullOrEmpty(Text);
            base.OnTextChanged(e);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public bool HasText
        {
            get => (bool)GetValue(HasTextProperty);
            private set => SetValue(HasTextPropertyKey, value);
        }
    }

    public class SingleLine : TextBoxBase
    {
    }
    
    public sealed class HeaderedSingleLine : SingleLine
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
                                                                                               nameof(Header),
                                                                                               typeof(string),
                                                                                               typeof(HeaderedSingleLine), 
                                                                                               new PropertyMetadata(default(string)));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
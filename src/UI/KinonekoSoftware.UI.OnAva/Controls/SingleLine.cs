namespace KinonekoSoftware.UI.Controls
{
    public abstract class TextBoxBase : TextBox
    {
        public static readonly DirectProperty<TextBoxBase, bool> HasTextProperty;

        static TextBoxBase()
        {
            HasTextProperty = AvaloniaProperty.RegisterDirect<TextBoxBase, bool>(nameof(HasText), x => x.HasText);
            TextProperty.Changed.AddClassHandler<TextBoxBase>((x, v) =>
            {

                x.HasText = !string.IsNullOrEmpty(v.GetNewValue<string>());
                x.OnTextChanged();
            });
        }

        private bool _hasText;

        protected virtual void OnTextChanged()
        {
            
        }


        public bool HasText
        {
            get => _hasText;
            set => SetAndRaise(HasTextProperty, ref _hasText, value);
        }
    }

    public class SingleLine : TextBoxBase
    {

    }

    public sealed class HeaderedSingleLine : SingleLine
    {
        public static readonly StyledProperty<string> HeaderProperty = AvaloniaProperty.Register<HeaderedSingleLine, string>(nameof(Header));

        public string Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
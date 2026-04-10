namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class TextButton : ButtonBase
    {
        public static readonly StyledProperty<IBrush> HighlightStateProperty;
        public static readonly StyledProperty<IBrush> HoverStateProperty;
        public static readonly StyledProperty<IBrush> DisabledStateProperty;

        static TextButton()
        {
            HighlightStateProperty = AvaloniaProperty.Register<TextButton, IBrush>(nameof(HighlightState));
            HoverStateProperty     = AvaloniaProperty.Register<TextButton, IBrush>(nameof(HoverState));
            DisabledStateProperty  = AvaloniaProperty.Register<TextButton, IBrush>(nameof(DisabledState));
        }

        [Bindable(true)]
        [Category("Appearance")]
        public IBrush DisabledState
        {
            get => GetValue(DisabledStateProperty);
            set => SetValue(DisabledStateProperty, value);
        }

        [Bindable(true)]
        [Category("Appearance")]
        public IBrush HoverState
        {
            get => GetValue(HoverStateProperty);
            set => SetValue(HoverStateProperty, value);
        }

        [Bindable(true)]
        [Category("Appearance")]
        public IBrush HighlightState
        {
            get => GetValue(HighlightStateProperty);
            set => SetValue(HighlightStateProperty, value);
        }
    }
}
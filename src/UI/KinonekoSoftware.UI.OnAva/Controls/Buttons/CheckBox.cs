namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class CheckBox : Avalonia.Controls.CheckBox
    {
        public static readonly StyledProperty<IBrush> HighlightProperty;

        static CheckBox()
        {
            HighlightProperty = AvaloniaProperty.Register<CheckBox, IBrush>(nameof(Highlight));
        }


        [Bindable(true)]
        [Category("Appearance")]
        public IBrush Highlight
        {
            get => GetValue(HighlightProperty);
            set => SetValue(HighlightProperty, value);
        }
    }
}


namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class OptionButton :  System.Windows.Controls.RadioButton
    {

        public static readonly DependencyProperty HighlightStateProperty =
            DependencyProperty.Register(
                                        nameof(HighlightState),
                                        typeof(Brush),
                                        typeof(OptionButton),
                                        new PropertyMetadata(default(Brush)));


        public static readonly DependencyProperty HoverStateProperty =
            DependencyProperty.Register(
                                        nameof(HoverState),
                                        typeof(Brush),
                                        typeof(OptionButton),
                                        new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty DisabledStateProperty =
            DependencyProperty.Register(
                                        nameof(DisabledState),
                                        typeof(Brush),
                                        typeof(OptionButton),
                                        new PropertyMetadata(default(Brush)));

        [Bindable(true)]
        [Category("Appearance")]
        public Brush DisabledState
        {
            get => (Brush)GetValue(DisabledStateProperty);
            set => SetValue(DisabledStateProperty, value);
        }
        
        [Bindable(true)]
        [Category("Appearance")]
        public Brush HoverState
        {
            get => (Brush)GetValue(HoverStateProperty);
            set => SetValue(HoverStateProperty, value);
        }
        
        [Bindable(true)]
        [Category("Appearance")]
        public Brush HighlightState
        {
            get => (Brush)GetValue(HighlightStateProperty);
            set => SetValue(HighlightStateProperty, value);
        }
    }
}
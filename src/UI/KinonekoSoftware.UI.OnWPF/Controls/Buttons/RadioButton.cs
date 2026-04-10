namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class RadioButton : System.Windows.Controls.RadioButton
    {
        public static readonly DependencyProperty HighlightStateProperty;
        public static readonly DependencyProperty HoverStateProperty;
        public static readonly DependencyProperty DisabledStateProperty;

        static RadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButton), new FrameworkPropertyMetadata(typeof(RadioButton))); 
            HighlightStateProperty =
                DependencyProperty.Register(
                                            nameof(HighlightState),
                                            typeof(Brush),
                                            typeof(RadioButton),
                                            new PropertyMetadata(default(Brush)));
            HoverStateProperty =
                DependencyProperty.Register(
                                            nameof(HoverState),
                                            typeof(Brush),
                                            typeof(RadioButton),
                                            new PropertyMetadata(default(Brush)));
            DisabledStateProperty =
                DependencyProperty.Register(
                                            nameof(DisabledState),
                                            typeof(Brush),
                                            typeof(RadioButton),
                                            new PropertyMetadata(default(Brush)));
        }

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
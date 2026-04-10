using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class ToggleButton : System.Windows.Controls.Primitives.ToggleButton
    {
        public static readonly DependencyProperty IconModeProperty;
        public static readonly DependencyProperty HighlightStateProperty;
        public static readonly DependencyProperty HoverStateProperty;
        public static readonly DependencyProperty DisabledStateProperty;

        static ToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleButton), new FrameworkPropertyMetadata(typeof(ToggleButton)));
            IconModeProperty =
                DependencyProperty.Register(
                                            nameof(IconMode),
                                            typeof(IconMode),
                                            typeof(ToggleButton),
                                            new PropertyMetadata(default(IconMode)));
            HighlightStateProperty =
                DependencyProperty.Register(
                                            nameof(HighlightState),
                                            typeof(Brush),
                                            typeof(ToggleButton),
                                            new PropertyMetadata(default(Brush)));
            HoverStateProperty =
                DependencyProperty.Register(
                                            nameof(HoverState),
                                            typeof(Brush),
                                            typeof(ToggleButton),
                                            new PropertyMetadata(default(Brush)));
            DisabledStateProperty =
                DependencyProperty.Register(
                                            nameof(DisabledState),
                                            typeof(Brush),
                                            typeof(ToggleButton),
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
        
        [Bindable(true)]
        [Category("Appearance")]
        public IconMode IconMode
        {
            get => (IconMode)GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
        }
    }
}
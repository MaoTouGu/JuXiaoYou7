using System.ComponentModel;
using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class TextButton : ButtonBase
    {
        public static readonly DependencyProperty HighlightStateProperty;
        public static readonly DependencyProperty HoverStateProperty;
        public static readonly DependencyProperty DisabledStateProperty;

        static TextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextButton), new FrameworkPropertyMetadata(typeof(TextButton)));
            HighlightStateProperty =
                DependencyProperty.Register(
                                            nameof(HighlightState),
                                            typeof(Brush),
                                            typeof(TextButton),
                                            new PropertyMetadata(default(Brush)));
            HoverStateProperty =
                DependencyProperty.Register(
                                            nameof(HoverState),
                                            typeof(Brush),
                                            typeof(TextButton),
                                            new PropertyMetadata(default(Brush)));
            DisabledStateProperty =
                DependencyProperty.Register(
                                            nameof(DisabledState),
                                            typeof(Brush),
                                            typeof(TextButton),
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
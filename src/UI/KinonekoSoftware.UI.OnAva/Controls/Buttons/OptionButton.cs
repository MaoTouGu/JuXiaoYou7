using System.ComponentModel;
using Avalonia;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class OptionButton : Avalonia.Controls.RadioButton
    {
        public static readonly StyledProperty<IBrush> HighlightStateProperty;
        public static readonly StyledProperty<IBrush> HoverStateProperty;
        public static readonly StyledProperty<IBrush> DisabledStateProperty;

        static OptionButton()
        {
            HighlightStateProperty = AvaloniaProperty.Register<OptionButton, IBrush>(nameof(HighlightState));
            HoverStateProperty     = AvaloniaProperty.Register<OptionButton, IBrush>(nameof(HoverState));
            DisabledStateProperty  = AvaloniaProperty.Register<OptionButton, IBrush>(nameof(DisabledState));
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
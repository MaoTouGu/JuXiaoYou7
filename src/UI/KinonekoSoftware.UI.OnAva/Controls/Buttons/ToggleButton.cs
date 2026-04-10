
using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class ToggleButton : Avalonia.Controls.Primitives.ToggleButton
    {
        
        public static readonly StyledProperty<IconMode> IconModeProperty;
        public static readonly StyledProperty<IBrush>   HighlightStateProperty;
        public static readonly StyledProperty<IBrush>   HoverStateProperty;
        public static readonly StyledProperty<IBrush>   DisabledStateProperty;

        static ToggleButton()
        {
            IconModeProperty = AvaloniaProperty.Register<ToggleButton, IconMode>(nameof(IconMode));
            
            HighlightStateProperty = AvaloniaProperty.Register<ToggleButton, IBrush>(nameof(HighlightState));
            HoverStateProperty     = AvaloniaProperty.Register<ToggleButton, IBrush>(nameof(HoverState));
            DisabledStateProperty  = AvaloniaProperty.Register<ToggleButton, IBrush>(nameof(DisabledState));
        }

        [Bindable(true)]
        [Category("Appearance")]
        public IconMode IconMode
        {
            get => GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
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
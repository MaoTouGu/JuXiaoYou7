using Avalonia;
using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public abstract class ButtonBase : Button
    {

        public static readonly StyledProperty<IconMode> IconModeProperty;

        static ButtonBase()
        {
            IconModeProperty = AvaloniaProperty.Register<ButtonBase, IconMode>(nameof(IconMode));
        }

        public IconMode IconMode
        {
            get => GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
        }
    }
}
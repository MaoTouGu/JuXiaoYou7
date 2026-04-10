using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls.Buttons
{
    public abstract class ButtonBase : Button
    {

        public static readonly DependencyProperty IconModeProperty =
            DependencyProperty.Register(
                                        nameof(IconMode),
                                        typeof(IconMode),
                                        typeof(ButtonBase),
                                        new PropertyMetadata(default(IconMode)));

        public IconMode IconMode
        {
            get => (IconMode)GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
        }
    }
}
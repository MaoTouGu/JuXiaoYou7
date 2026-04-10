namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class ToggleSwitch : System.Windows.Controls.Primitives.ToggleButton
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }
    }
}
namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class MainButton : ButtonBase
    {
        static MainButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainButton), new FrameworkPropertyMetadata(typeof(MainButton)));
        }
    }
}
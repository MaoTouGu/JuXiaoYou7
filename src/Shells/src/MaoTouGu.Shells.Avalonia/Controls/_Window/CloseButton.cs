
using Button = System.Windows.Controls.Button;

namespace MaoTouGu.Shells.Controls
{
    public sealed class CloseButton : Button
    {
        static CloseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton), new FrameworkPropertyMetadata(typeof(CloseButton))); 
        }
    }
}
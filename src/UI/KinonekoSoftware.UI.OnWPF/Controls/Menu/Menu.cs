

namespace KinonekoSoftware.UI.Controls
{
    public class MainMenu : Menu
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }
    }

    public class ContextMenu : System.Windows.Controls.ContextMenu
    {
        protected override bool IsItemItsOwnContainerOverride(object item) => item is FrameworkElement;

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }
    }
}
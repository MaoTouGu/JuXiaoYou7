
namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class ListBox : System.Windows.Controls.ListBox
    {
        
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is FrameworkElement;
    }
    
    
    public sealed class ListBoxItem : System.Windows.Controls.ListBoxItem
    {
        private Selector ParentSelector => ItemsControl.ItemsControlFromItemContainer(this) as Selector;
        
        protected override void OnMouseEnter(MouseEventArgs e)
        {
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            ParentSelector?.ReleaseMouseCapture();
        }
    }
}
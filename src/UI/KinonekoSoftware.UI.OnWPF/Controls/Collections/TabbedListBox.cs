namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class TabbedListBox : System.Windows.Controls.ListBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabbedListBoxItem();
        }
        
        protected override bool IsItemItsOwnContainerOverride(object item) => item is TabbedListBoxItem;
    }

    public sealed class TabbedListBoxItem : System.Windows.Controls.ListBoxItem
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
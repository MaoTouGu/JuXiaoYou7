namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class OptionListBox : System.Windows.Controls.ListBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new OptionListBoxItem();
        }
        
        protected override bool IsItemItsOwnContainerOverride(object item) => item is FrameworkElement;
    }
    
    
    public sealed class OptionListBoxItem : System.Windows.Controls.ListBoxItem
    {
        
    }
}
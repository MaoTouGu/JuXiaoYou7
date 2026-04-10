namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class TabbedListBox : Avalonia.Controls.ListBox
    {
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<TabbedListBoxItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new TabbedListBoxItem();
    }

    public sealed class TabbedListBoxItem : Avalonia.Controls.ListBoxItem
    {
        
    }
}
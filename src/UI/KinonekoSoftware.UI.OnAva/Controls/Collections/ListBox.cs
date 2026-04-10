using Avalonia.Styling;

namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class ListBox : Avalonia.Controls.ListBox
    {
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<ListBoxItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new ListBoxItem();
    }


    public sealed class ListBoxItem : Avalonia.Controls.ListBoxItem
    {
        private ItemsControl ParentSelector => ItemsControl.ItemsControlFromItemContainer(this);
        
    }
}
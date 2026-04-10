namespace KinonekoSoftware.UI.Controls.Collections
{
    public class ListView : Avalonia.Controls.ListBox
    {
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<ListBoxItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new ListBoxItem();
    }
}
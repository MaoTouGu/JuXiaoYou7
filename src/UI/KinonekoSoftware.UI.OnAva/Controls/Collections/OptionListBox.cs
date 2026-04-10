namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class OptionListBox: Avalonia.Controls.ListBox
    {
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<OptionListBoxItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new OptionListBoxItem();
    }


    public sealed class OptionListBoxItem : Avalonia.Controls.ListBoxItem
    {

    }
}
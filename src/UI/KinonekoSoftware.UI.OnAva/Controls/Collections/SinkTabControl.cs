namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class SinkTabControl : TabControl
    {
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<SinkTabItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new SinkTabItem();
    }

    public sealed class SinkTabItem : TabItem
    {
    }
}
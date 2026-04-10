namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class IconifyTabControl : TabControl
    {

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new IconifyTabItem();
    }

    public sealed class IconifyTabItem : TabItem
    {
    }
}
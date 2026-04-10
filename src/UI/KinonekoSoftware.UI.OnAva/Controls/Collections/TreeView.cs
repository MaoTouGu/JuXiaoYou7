namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class TreeView : Avalonia.Controls.TreeView
    {
        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new TreeViewItem();
    }
    
    public sealed class TreeViewItem : Avalonia.Controls.TreeViewItem
    {
        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new TreeViewItem();
    }
}
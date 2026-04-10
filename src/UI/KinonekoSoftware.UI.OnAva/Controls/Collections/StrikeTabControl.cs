namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class StrikeTabControl : TabControl
    {
        
        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<StrikeTabItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new StrikeTabItem();
    }
    
    public class StrikeTabItem : TabItem
    {
    }
}
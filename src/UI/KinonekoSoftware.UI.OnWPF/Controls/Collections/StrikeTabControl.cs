namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class StrikeTabControl : TabControl
    {
        static StrikeTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StrikeTabControl), new FrameworkPropertyMetadata(typeof(StrikeTabControl)));
        }
        
        protected override DependencyObject GetContainerForItemOverride() => new StrikeTabItem();
    }
    
    public class StrikeTabItem : TabItem
    {
        static StrikeTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StrikeTabItem), new FrameworkPropertyMetadata(typeof(StrikeTabItem)));
        }
    }
}
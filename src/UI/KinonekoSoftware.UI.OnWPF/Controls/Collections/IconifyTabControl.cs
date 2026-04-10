namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class IconifyTabControl : TabControl
    {
        static IconifyTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconifyTabControl), new FrameworkPropertyMetadata(typeof(IconifyTabControl)));
        }
        
        protected override DependencyObject GetContainerForItemOverride() => new IconifyTabItem();
    }

    public sealed class IconifyTabItem : TabItem
    {
        static IconifyTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconifyTabItem), new FrameworkPropertyMetadata(typeof(IconifyTabItem)));
        }
    }
}
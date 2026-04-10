namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class SinkTabControl : TabControl
    {
        static SinkTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SinkTabControl), new FrameworkPropertyMetadata(typeof(SinkTabControl)));
        }

        protected override DependencyObject GetContainerForItemOverride() => new SinkTabItem();
    }

    public sealed class SinkTabItem : TabItem
    {
        static SinkTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SinkTabItem), new FrameworkPropertyMetadata(typeof(SinkTabItem)));
        }
    }
}
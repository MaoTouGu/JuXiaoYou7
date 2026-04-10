using Avalonia.Controls.Templates;

namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class AdvancedSinkTabControl : TabControl
    {

        public static readonly StyledProperty<object>        ToolBarProperty;
        public static readonly StyledProperty<IDataTemplate> ToolBarTemplateProperty;
        public static readonly StyledProperty<Thickness>     ToolBarMarginProperty;

        static AdvancedSinkTabControl()
        {
            ToolBarProperty         = AvaloniaProperty.Register<AdvancedSinkTabControl, object>(nameof(ToolBar));
            ToolBarTemplateProperty = AvaloniaProperty.Register<AdvancedSinkTabControl, IDataTemplate>(nameof(ToolBarTemplate));
            ToolBarMarginProperty   = AvaloniaProperty.Register<AdvancedSinkTabControl, Thickness>(nameof(ToolBarMargin));

        }

        protected override bool NeedsContainerOverride(object item, int index, out object recycleKey) => NeedsContainer<SinkTabItem>(item, out recycleKey);

        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new SinkTabItem();




        public Thickness ToolBarMargin
        {
            get => GetValue(ToolBarMarginProperty);
            set => SetValue(ToolBarMarginProperty, value);
        }


        /// <summary>
        /// 获取或设置 <see cref="ToolBarTemplate"/> 属性。
        /// </summary>
        public IDataTemplate ToolBarTemplate
        {
            get => GetValue(ToolBarTemplateProperty);
            set => SetValue(ToolBarTemplateProperty, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="ToolBar"/> 属性。
        /// </summary>
        public object ToolBar
        {
            get => GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }

    }
}
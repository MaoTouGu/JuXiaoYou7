namespace KinonekoSoftware.UI.Controls.Collections
{
    public class AdvancedSinkTabControl : TabControl
    {
        
        public static readonly DependencyProperty ToolBarProperty;
        public static readonly DependencyProperty ToolBarTemplateProperty;
        public static readonly DependencyProperty ToolBarTemplateSelectorProperty;
        public static readonly DependencyProperty ToolBarTemplateStringProperty;
        public static readonly DependencyProperty ToolBarMarginProperty;
        
        static AdvancedSinkTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedSinkTabControl), new FrameworkPropertyMetadata(typeof(AdvancedSinkTabControl)));
            ToolBarProperty = DependencyProperty.Register(
                                                          nameof(ToolBar),
                                                          typeof(object),
                                                          typeof(AdvancedSinkTabControl),
                                                          new PropertyMetadata(default(object)));

            ToolBarTemplateProperty = DependencyProperty.Register(
                                                                   nameof(ToolBarTemplate),
                                                                   typeof(DataTemplate),
                                                                   typeof(AdvancedSinkTabControl),
                                                                   new PropertyMetadata(default(DataTemplate)));
            
            ToolBarTemplateSelectorProperty = DependencyProperty.Register(
                                                                nameof(ToolBarTemplateSelector),
                                                                typeof(DataTemplateSelector),
                                                                typeof(AdvancedSinkTabControl),
                                                                new PropertyMetadata(default(DataTemplateSelector)));
            
            ToolBarTemplateStringProperty = DependencyProperty.Register(
                                                                        nameof(ToolBarTemplateString),
                                                                        typeof(string),
                                                                        typeof(AdvancedSinkTabControl),
                                                                        new PropertyMetadata(default(string)));


            ToolBarMarginProperty = DependencyProperty.Register(
                                                                nameof(ToolBarMargin),
                                                                typeof(Thickness),
                                                                typeof(AdvancedSinkTabControl),
                                                                new PropertyMetadata(default(Thickness)));

        }


        protected override DependencyObject GetContainerForItemOverride() => new SinkTabItem();

        
        public Thickness ToolBarMargin
        {
        get => (Thickness)GetValue(ToolBarMarginProperty);
        set => SetValue(ToolBarMarginProperty, value);
        }
        
        public string ToolBarTemplateString
        {
            get => (string)GetValue(ToolBarTemplateStringProperty);
            set => SetValue(ToolBarTemplateStringProperty, value);
        }

        public DataTemplateSelector ToolBarTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ToolBarTemplateSelectorProperty);
            set => SetValue(ToolBarTemplateSelectorProperty, value);
        }

        public DataTemplate ToolBarTemplate
        {
            get => (DataTemplate)GetValue(ToolBarTemplateProperty);
            set => SetValue(ToolBarTemplateProperty, value);
        }
        
        public object ToolBar
        {
            get => GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }
        
    }
}
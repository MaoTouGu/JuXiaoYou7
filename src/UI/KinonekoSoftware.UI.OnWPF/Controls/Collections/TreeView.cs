namespace KinonekoSoftware.UI.Controls.Collections
{
    public sealed class TreeView : System.Windows.Controls.TreeView
    {
        public static readonly DependencyProperty BindableSelectedItemProperty = DependencyProperty.Register(
                                                                                                             nameof(BindableSelectedItem),
                                                                                                             typeof(object),
                                                                                                             typeof(TreeView),
                                                                                                             new PropertyMetadata(default(object)));
        
        static TreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeView), new FrameworkPropertyMetadata(typeof(TreeView)));
        }
        
        protected override DependencyObject GetContainerForItemOverride() => new TreeViewItem();

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            BindableSelectedItem = SelectedValue;
            base.OnSelectedItemChanged(e);
        }


        public object BindableSelectedItem
        {
            get => GetValue(BindableSelectedItemProperty);
            set => SetValue(BindableSelectedItemProperty, value);
        }
    }
    
    public sealed class TreeViewItem : System.Windows.Controls.TreeViewItem
    {
        protected override DependencyObject GetContainerForItemOverride() => new TreeViewItem();
    }
}
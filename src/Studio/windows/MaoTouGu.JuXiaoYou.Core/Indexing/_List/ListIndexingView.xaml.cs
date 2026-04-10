namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(ListIndexingView), ViewModel = typeof(ListViewModel))]
    public partial class ListIndexingView : ForestPage
    {
        public ListIndexingView()
        {
            InitializeComponent();
        }

        private void MenuItem_PseudoExecute(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement { DataContext: PseudoCommandItem command })
            {
                var dc = ViewModel<ListViewModel>();

                dc.Moniker = dc.Monikers.FirstOrDefault();
                dc.VisualManager?.Execute(dc, dc.Moniker, command);
            }
        }
    }
}
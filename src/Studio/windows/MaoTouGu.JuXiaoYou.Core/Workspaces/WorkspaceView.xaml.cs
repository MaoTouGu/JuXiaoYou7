namespace MaoTouGu.JuXiaoYou.Workspaces
{

    [Associate(View = typeof(WorkspaceView), ViewModel = typeof(WorkspaceViewModel))]
    public partial class WorkspaceView : ForestPage
    {
        public WorkspaceView()
        {
            InitializeComponent();
        }
        
        private void Button_CloseTab(object sender, RoutedEventArgs e)
        {
            if (sender is not Button { DataContext: NestedPage page })
            {
                return;
            }

            page.Dispose();



            var dc = ViewModel<WorkspaceViewModel>();
            var index = dc.Tabs.IndexOf(page);

            dc.Tabs.Remove(page);
            
            if (index < dc.Tabs.Count)
            {
                dc.Tab = dc.Tabs[index];
            }
            else
            {
                dc.Tab = dc.Tabs.LastOrDefault();
            }
        }
    }
}
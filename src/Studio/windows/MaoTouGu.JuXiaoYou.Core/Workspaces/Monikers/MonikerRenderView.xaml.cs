namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{

    [Associate(View = typeof(MonikerRenderView), ViewModel = typeof(MonikerRenderViewModel))]
    public partial class MonikerRenderView : ForestPage
    {
        public MonikerRenderView()
        {
            InitializeComponent();
        }
    }
}
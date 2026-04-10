namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{

    [Associate(View = typeof(MonikerTransitView), ViewModel = typeof(MonikerTransitViewModel))]
    public partial class MonikerTransitView : ForestPage
    {
        public MonikerTransitView()
        {
            InitializeComponent();
        }
    }
}
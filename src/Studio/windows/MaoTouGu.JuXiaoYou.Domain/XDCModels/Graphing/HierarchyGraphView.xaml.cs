namespace MaoTouGu.JuXiaoYou.XDCModels.Graphing
{

    [Associate(View = typeof(HierarchyGraphView), ViewModel = typeof(HierarchyGraphViewModel))]
    public partial class HierarchyGraphView : ForestPage
    {
        public HierarchyGraphView()
        {
            InitializeComponent();
        }
    }
}
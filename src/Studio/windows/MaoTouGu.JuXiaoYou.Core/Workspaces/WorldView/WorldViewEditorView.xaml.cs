namespace MaoTouGu.JuXiaoYou.Workspaces.WorldView
{

    [Associate(View = typeof(WorldViewEditorView), ViewModel = typeof(WorldViewEditorViewModel))]
    public partial class WorldViewEditorView : ForestPage
    {
        public WorldViewEditorView()
        {
            InitializeComponent();
        }
    }
}
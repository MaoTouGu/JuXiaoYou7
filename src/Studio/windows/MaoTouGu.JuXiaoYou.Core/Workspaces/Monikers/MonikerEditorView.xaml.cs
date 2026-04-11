namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{

    [Associate(View = typeof(MonikerEditorView), ViewModel = typeof(MonikerEditorViewModel))]
    public partial class MonikerEditorView : ForestPage
    {
        public MonikerEditorView()
        {
            InitializeComponent();
        }
    }
}
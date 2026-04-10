namespace MaoTouGu.JuXiaoYou.Startups
{

    [Associate(View = typeof(DomainEditorView), ViewModel = typeof(DomainEditorViewModel))]
    public partial class DomainEditorView : ForestDialog
    {
        public DomainEditorView()
        {
            InitializeComponent();
        }
    }
}
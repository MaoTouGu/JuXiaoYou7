

namespace MaoTouGu.JuXiaoYou.Services.CFE
{

    [Associate(View = typeof(CompositableFormDrawer), ViewModel = typeof(CompositableFormDrawerRoot))]
    [Associate(View = typeof(CompositableFormDrawer), ViewModel = typeof(CompositableFormDialogRoot))]
    public partial class CompositableFormDrawer : ForestDialog
    {
        public CompositableFormDrawer()
        {
            InitializeComponent();
        }
    }
}
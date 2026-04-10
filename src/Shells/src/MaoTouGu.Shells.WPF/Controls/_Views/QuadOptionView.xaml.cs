namespace MaoTouGu.Shells.Controls
{

    [Associate(View = typeof(QuadOptionView), ViewModel = typeof(QuadOptionRoot))]
    public partial class QuadOptionView : ForestDialog
    {
        public QuadOptionView()
        {
            InitializeComponent();
        }
    }
}
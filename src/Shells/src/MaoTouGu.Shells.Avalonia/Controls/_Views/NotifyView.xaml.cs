namespace MaoTouGu.Shells.Controls
{

    [Associate(View = typeof(NotifyView), ViewModel = typeof(NotifyRoot))]
    public partial class NotifyView : ForestDialog
    {
        public NotifyView()
        {
            InitializeComponent();
        }
    }
}
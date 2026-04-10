namespace MaoTouGu.Shells.Controls
{

    [Associate(View = typeof(EnumPickerView), ViewModel = typeof(EnumPickerRoot<>))]
    public partial class EnumPickerView : ForestDialog
    {
        public EnumPickerView()
        {
            InitializeComponent();
        }
    }
}
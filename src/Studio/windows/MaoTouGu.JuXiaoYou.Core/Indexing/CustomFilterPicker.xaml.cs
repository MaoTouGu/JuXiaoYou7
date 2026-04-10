namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(CustomFilterPicker), ViewModel = typeof(CustomFilterPickerViewModel))]
    public partial class CustomFilterPicker : ForestDialog
    {
        public CustomFilterPicker()
        {
            InitializeComponent();
        }
    }
}
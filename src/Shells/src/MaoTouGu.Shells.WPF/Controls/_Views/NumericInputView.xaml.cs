namespace MaoTouGu.Shells.Controls
{

    [Associate(View = typeof(NumericInputView), ViewModel = typeof(RangeInputRoot))]
    public partial class NumericInputView : ForestDialog
    {
        public NumericInputView()
        {
            InitializeComponent();
        }
    }
}
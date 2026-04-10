namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(IndexingOptionView), ViewModel = typeof(IndexingOptionViewModel))]
    public partial class IndexingOptionView : ForestDialog
    {
        public IndexingOptionView()
        {
            InitializeComponent();
        }
    }
}
namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(ByStarRankingView), ViewModel = typeof(ByStarRankingViewModel))]
    public partial class ByStarRankingView : ForestPage
    {
        public ByStarRankingView()
        {
            InitializeComponent();
        }
    }
}
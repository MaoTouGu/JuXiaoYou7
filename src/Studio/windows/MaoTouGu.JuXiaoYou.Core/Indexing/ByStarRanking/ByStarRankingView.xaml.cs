namespace MaoTouGu.JuXiaoYou.Indexing.BySetting
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
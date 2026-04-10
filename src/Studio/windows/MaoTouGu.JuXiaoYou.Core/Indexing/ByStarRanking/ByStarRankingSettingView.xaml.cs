using MaoTouGu.JuXiaoYou.Indexing;

namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(ByStarRankingSettingView), ViewModel = typeof(ByStarRankingSettingViewModel))]
    public partial class ByStarRankingSettingView : ForestPage
    {
        public ByStarRankingSettingView()
        {
            InitializeComponent();
        }
    }
}
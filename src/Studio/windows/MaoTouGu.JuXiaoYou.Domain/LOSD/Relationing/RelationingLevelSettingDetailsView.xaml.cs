namespace MaoTouGu.JuXiaoYou.LOSD.Relationing
{

    [Associate(View = typeof(RelationingLevelSettingDetailsView), ViewModel = typeof(RelationingLevelSettingDetailsViewModel))]
    public partial class RelationingLevelSettingDetailsView : ForestPage
    {
        public RelationingLevelSettingDetailsView()
        {
            InitializeComponent();
        }
    }
}
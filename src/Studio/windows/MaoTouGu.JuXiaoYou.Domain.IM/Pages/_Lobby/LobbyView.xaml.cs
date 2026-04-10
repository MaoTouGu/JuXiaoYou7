namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages
{

    [Associate(View = typeof(LobbyView), ViewModel = typeof(LobbyViewModel))]
    public partial class LobbyView : ForestPage
    {
        public LobbyView()
        {
            InitializeComponent();
        }
    }
}
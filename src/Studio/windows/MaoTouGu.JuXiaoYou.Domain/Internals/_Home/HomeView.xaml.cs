namespace MaoTouGu.JuXiaoYou.Internals
{

    [Associate(View = typeof(HomeView), ViewModel = typeof(HomeViewModel))]
    public partial class HomeView : ForestPage
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}
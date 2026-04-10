namespace MaoTouGu.JuXiaoYou.Startups
{

    [Associate(View = typeof(DomainView), ViewModel = typeof(DomainViewModel))]
    public partial class DomainView : ForestPage
    {
        public DomainView()
        {
            InitializeComponent();
        }
    }
}